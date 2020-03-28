namespace Lab04.Tools.Navigation
{
    internal enum ViewType
    {
        Add = 0,
        Edit = 1,
        Main = 2,
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}