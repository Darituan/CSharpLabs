using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Lab04.Models;

namespace Lab04.Tools.SortAndFilter
{
    internal static class SortAndFilter
    {
        internal static ObservableCollection<Person> SortAndFilterUsers(IEnumerable<Person> users,
            Dictionary<MethodInfo, IComparable> lowerBounds, 
            Dictionary<MethodInfo, IComparable> higherBounds, Dictionary<MethodInfo, string> containedStrings,
            Dictionary<MethodInfo, bool> predicates, MethodInfo sortGetter)
        {
            var sortedAndFiltered = from user in users
                where Match(user, lowerBounds, higherBounds, containedStrings, predicates)
                orderby sortGetter.Invoke(user, null)
                select user;
            return new ObservableCollection<Person>(sortedAndFiltered);
        }
        
        internal static ObservableCollection<Person> SortAndFilterUsers(IEnumerable<Person> users,
            Dictionary<MethodInfo, IComparable> lowerBounds, 
            Dictionary<MethodInfo, IComparable> higherBounds, Dictionary<MethodInfo, string> containedStrings,
            Dictionary<MethodInfo, bool> predicates)
        {
            var sortedAndFiltered = from user in users
                where Match(user, lowerBounds, higherBounds, containedStrings, predicates)
                select user;
            return new ObservableCollection<Person>(sortedAndFiltered);
        }

        private static bool Match(Person user, Dictionary<MethodInfo, IComparable> lowerBounds, 
            Dictionary<MethodInfo, IComparable> higherBounds, Dictionary<MethodInfo, string> containedStrings,
            Dictionary<MethodInfo, bool> predicates)
        {
            foreach (var (key, value) in lowerBounds)
            {
                if ((key.Invoke(user, null) as IComparable)?.CompareTo(value) < 0)
                    return false;
            }
            
            foreach (var (key, value) in higherBounds)
            {
                if ((key.Invoke(user, null) as IComparable)?.CompareTo(value) > 0)
                    return false;
            }
            
            foreach (var (key, value) in containedStrings)
            {
                if (key.Invoke(user, null) is string propertyValue && !value.Contains(propertyValue))
                    return false;
            }
            
            foreach (var (key, value) in predicates)
            {
                if (key.Invoke(user, null) is bool propertyValue && value != propertyValue)
                    return false;
            }

            return true;
        }
    }
}