using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FolderDesigner.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel()
        {
            SelectedMediaType = MediaType.Tv;
            ConsoleOutput = String.Empty;
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
                    WriteLineToConsole("Decorate button pressed!");
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

        private void WriteLineToConsole(string line)
        {
            ConsoleOutput += line;
            ConsoleOutput += System.Environment.NewLine;
            OnPropertyChanged("ConsoleOutput");
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
