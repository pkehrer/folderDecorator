﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace FolderDesigner.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly FolderDecorator _folderDecorator;

        public HomeViewModel()
        {
            SelectedMediaType = MediaType.Tv;
            ConsoleOutput = String.Empty;
            CurrentDirectory = @"E:\TV";
            _folderDecorator =
                new FolderDecorator(
                        new IconMaker(
                        new TheTvDbRetriever(),
                        new ImageCropper(),
                        new ImageResizer(),
                        new IconConverter()),
                    new DesktopIniMaker(),
                    new SystemFolderitizer());
        }

        public ICommand BrowseCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var fbd = new FolderBrowserDialog();
                    fbd.ShowNewFolderButton = false;
                    var result = fbd.ShowDialog();
                    CurrentDirectory = fbd.SelectedPath;
                });
            }
        }

        public ICommand DecorateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var bw = new BackgroundWorker();
                    bw.WorkerReportsProgress = true;
                    bw.DoWork += Worker_DoWork;
                    bw.RunWorkerCompleted += Worker_WorkerCompleted;
                    bw.ProgressChanged += worker_ProgressChanged;
                    bw.RunWorkerAsync(Directory.GetDirectories(CurrentDirectory));
                });
            }
        }

        void Worker_DoWork(object sender, DoWorkEventArgs args)
        {
            var directories = (IEnumerable<string>)args.Argument;
            foreach (var dir in directories)
            {
                (sender as BackgroundWorker).ReportProgress(0, "Decorating " + dir + "...");
                try
                {
                    _folderDecorator.DecorateFolder(dir);
                    (sender as BackgroundWorker).ReportProgress(0, "Done! " + System.Environment.NewLine);
                }
                catch (Exception e)
                {
                    (sender as BackgroundWorker).ReportProgress(0, "Error: " + System.Environment.NewLine + e.Message + System.Environment.NewLine);
                }
            }
            
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            var message = args.UserState as string;
            WriteToConsole(message);
        }

        void Worker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            WriteLineToConsole("Work Complete!");
        }

        private string _currentDirectory;
        public string CurrentDirectory
        {
            get
            {
                return _currentDirectory;
            }
            set
            {
                _currentDirectory = value;
                OnPropertyChanged("CurrentDirectory");
            }
        }

        public IEnumerable<MediaType> AllMediaTypes
        {
            get
            {
                return Enum.GetValues(typeof(MediaType)).Cast<MediaType>();
            }
        }

        private MediaType _selectedMediaType;
        public MediaType SelectedMediaType
        {
            get { return _selectedMediaType; }
            set
            {
                _selectedMediaType = value;
                OnPropertyChanged("SelectedMediaType");
            }
        }

        private void WriteToConsole(string text, params string[] values)
        {
            ConsoleOutput += string.Format(text, values);
            OnPropertyChanged("ConsoleOutput");
        }

        private void WriteLineToConsole(string line, params string[] values)
        {
            WriteToConsole(line + System.Environment.NewLine, values);
        }

        public string ConsoleOutput
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}