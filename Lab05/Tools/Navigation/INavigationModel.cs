namespace Lab05.Tools.Navigation
{
    internal enum ViewType
    {
        Main = 0,
        Threads = 1,
        Modules = 2
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}