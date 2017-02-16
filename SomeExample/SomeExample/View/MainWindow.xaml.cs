using SomeExample.ViewModel;
using System.Windows;

namespace SomeExample.View
{
 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
