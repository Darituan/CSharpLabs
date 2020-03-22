using System;
using Lab04.Views;

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
                case ViewType.AddEdit:
                    AddView(ViewType.AddEdit, new AddEditView());
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