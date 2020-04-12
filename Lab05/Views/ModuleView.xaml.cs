using System.Windows.Controls;
using Lab05.Tools.Navigation;
using Lab05.ViewModels;

namespace Lab05.Views
{
    public partial class ModuleView : UserControl, INavigatable
    {
        public ModuleView()
        {
            InitializeComponent();
            DataContext = new ModulesViewModel();
        }
    }
}