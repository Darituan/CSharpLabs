using System.Windows.Controls;
using Lab05.Tools.Navigation;
using Lab05.ViewModels;

namespace Lab05.Views
{
    public partial class ThreadView : UserControl, INavigatable
    {
        public ThreadView()
        {
            InitializeComponent();
            DataContext = new ThreadsViewModel();
        }
    }
}