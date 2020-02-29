using System.Windows.Controls;
using Lab02.ViewModels.ZodiacDeterminant;

namespace Lab02.Views
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