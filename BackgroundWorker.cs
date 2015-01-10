using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CM = System.ComponentModel;

namespace FolderDesigner
{
    public class BackgroundWorker
    {
        readonly Action<string> _directoryAction;
        readonly CM.BackgroundWorker _backgroundWorker;
        readonly IEnumerable<string> _directories;

        public BackgroundWorker(IEnumerable<string> directories,
            Action<string> directoryAction,
            Action completionAction)
        {
            _directories = directories;
            _directoryAction = directoryAction;
            _backgroundWorker = new CM.BackgroundWorker();
            _backgroundWorker.DoWork += DoWork;
            _backgroundWorker.RunWorkerCompleted += (s, a) => completionAction();
        }

        public void Run()
        {
            _backgroundWorker.RunWorkerAsync(_directories);
        }

        private void DoWork(object sender, DoWorkEventArgs args)
        {
            var directories = args.Argument as IReadOnlyCollection<string>;
            if (directories == null) throw new InvalidOperationException("Background worker not started with list of strings.");

            if (Config.Multithreaded)
            {
                Parallel.ForEach(directories, _directoryAction);
            }
            else
            {
                foreach (var dir in directories)
                {
                    _directoryAction(dir);
                }
            }
        }
    }
}
