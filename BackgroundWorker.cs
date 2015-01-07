using System;
using System.Collections.Generic;
using CM = System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace FolderDesigner
{
    public class BackgroundWorker
    {

        private readonly Action<string> _directoryAction;
        private readonly Action<string> _progressAction;
        private readonly Action _completionAction;
        private readonly CM.BackgroundWorker _backgroundWorker;
        private readonly IEnumerable<string> _directories;
        
        public BackgroundWorker(IEnumerable<string> directories,
            Action<string> directoryAction, 
          //  Action<string> progressAction,
            Action completionAction)
        {
            _directories = directories;
            _directoryAction = directoryAction;
            _backgroundWorker = new CM.BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += DoWork;
            //_backgroundWorker.ProgressChanged += ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += (s,a) => completionAction();
        }

        public void Run()
        {
            _backgroundWorker.RunWorkerAsync(_directories);
        }

        private void DoWork(object sender, DoWorkEventArgs args)
        {
            var resetEvents = new List<ManualResetEvent>();
            var directories = (IEnumerable<string>)args.Argument;
            foreach (var dir in directories)
            {
                var resetEvent = new ManualResetEvent(false);
                resetEvents.Add(resetEvent);

                if (Config.Multithreaded)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(a =>
                    {
                        _directoryAction(dir);
                        resetEvent.Set();
                    }));
                }
                else
                {
                    _directoryAction(dir);
                }
            }
            if (Config.Multithreaded)
            {
                WaitHandle.WaitAll(resetEvents.ToArray());
            }
        }

        void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            var message = args.UserState as string;
            _progressAction(message);
        }
    }
}
