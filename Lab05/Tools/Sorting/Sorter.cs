using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Lab05.ViewModels;


namespace Lab05.Tools.Sorting
{
    internal static class Sorter
    {
        internal static ObservableCollection<ProcessViewModel> SortProcesses(IEnumerable<ProcessViewModel> processes,
            MethodInfo sortGetter)
        {
            var sorted = from process in processes
                orderby sortGetter.Invoke(process, null)
                select process;
            return new ObservableCollection<ProcessViewModel>(sorted);
        }
    }
}