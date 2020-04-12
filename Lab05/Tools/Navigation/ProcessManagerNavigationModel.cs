using System;
//using Lab05.Views;


namespace Lab05.Tools.Navigation
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
                case ViewType.Threads:
                    //AddView(ViewType.Threads, new ThreadsView());
                    break;
                case ViewType.Modules:
                    //AddView(ViewType.Modules, new ModulesView());
                    break;
                case ViewType.Main:
                    //AddView(ViewType.Main, new MainView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}