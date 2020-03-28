using System;
using Lab04.Views;
using Lab04.ViewModels;

namespace Lab04.Tools.Navigation
{
    internal class ZodiacDeterminantNavigationModel: BaseNavigationModel
    {
        public ZodiacDeterminantNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
            
        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Add:
                    AddView(ViewType.Add, new AddEditView(new AddEditViewModel(true)));
                    break;
                case ViewType.Edit:
                    AddView(ViewType.Edit, new AddEditView(new AddEditViewModel(false)));
                    break;
                case ViewType.Main:
                    AddView(ViewType.Main, new MainView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}