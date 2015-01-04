using FolderDesigner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;

namespace FolderDesigner.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            var container = Module.GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                DataContext = new HomeViewModel(scope.Resolve<FolderDecorator>(), scope.Resolve<FolderUndecorator>());
                InitializeComponent();
            }
        }
    }
}
