namespace Lab04.Tools.Navigation
{
    internal enum ViewType
    {
        AddEdit = 0,
        Main = 1,
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}