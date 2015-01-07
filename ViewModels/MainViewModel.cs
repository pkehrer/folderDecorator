using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly FolderDecorator _folderDecorator;
        private readonly FolderUndecorator _folderUndecorator;
        private readonly IconCacheResetter _iconCacheResetter;

        public MainViewModel(
            FolderDecorator folderDecorator, 
            FolderUndecorator folderUndecorator,
            IconCacheResetter iconCacheResetter)
        {
            SelectedMediaType = MediaType.Movie;
            ConsoleOutput = String.Empty;
            CurrentDirectory = @"E:\Movies";
            _folderDecorator = folderDecorator;
            _folderUndecorator = folderUndecorator;
            _iconCacheResetter = iconCacheResetter;
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
                    ClearConsole();
                    var bw = new BackgroundWorker(Directory.GetDirectories(CurrentDirectory),
                        dir =>
                        {
                            var result = _folderDecorator.DecorateFolder(SelectedMediaType, dir);
                            WriteLineToConsole(result.ToString());
                        },
                        () =>
                        {
                            WriteLineToConsole("Decoration complete!");
                            _iconCacheResetter.ResetIconCache();
                        });
                    bw.Run();
                });
                
            }
        }

        public ICommand UnDecorateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ClearConsole();
                    var bw = new BackgroundWorker(
                        Directory.GetDirectories(CurrentDirectory),
                        dir =>
                        {
                            _folderUndecorator.UndecorateFolder(dir);
                            WriteLineToConsole("Undecorated {0}", dir);
                        },
                        () =>
                        {
                            WriteLineToConsole("Undecoration complete!");
                            _iconCacheResetter.ResetIconCache();
                        });
                    bw.Run();
                });
            }
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

        private static object _syncobj = new Object();
        private void WriteToConsole(string text, params string[] values)
        {
            lock (_syncobj)
            {
                ConsoleOutput += values.Any() ? string.Format(text, values) : text;
            }
            OnPropertyChanged("ConsoleOutput");
        }

        private void WriteLineToConsole(string line, params string[] values)
        {
            WriteToConsole(line + System.Environment.NewLine, values);
        }

        private void ClearConsole()
        {
            ConsoleOutput = String.Empty;
            OnPropertyChanged("ConsoleOutput");
        }

        public string ConsoleOutput { get; set; }

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
