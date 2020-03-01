using System.Windows.Controls;
using Lab03.ViewModels.ZodiacDeterminant;

namespace Lab03.Views
{
    public partial class ZodiacDeterminantView : UserControl
    {
        public ZodiacDeterminantView()
        {
            InitializeComponent();
            DataContext = new ZodiacDeterminantViewModel();
        }
    }
}