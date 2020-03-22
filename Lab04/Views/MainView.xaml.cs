using System.Windows.Controls;
using Lab04.Tools.Navigation;
using Lab04.ViewModels;

namespace Lab04.Views
{
    public partial class MainView : UserControl, INavigatable
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}