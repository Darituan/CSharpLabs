using System.Windows.Controls;
using Lab04.Tools.Navigation;
using Lab04.ViewModels;

namespace Lab04.Views
{
    public partial class AddEditView : UserControl, INavigatable
    {
        public AddEditView(AddEditViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}