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
            List<MethodInfo> boundFilterGetters, List<MethodInfo> stringFilterGetters, 
            List<MethodInfo> boolFilterGetters, List<IComparable> lowerBounds, List<IComparable> higherBounds,
            List<string> stringKeys, List<bool?> boolKeys, MethodInfo sortGetter)
        {
            var sortedAndFiltered = from user in users
                where Match(user, boundFilterGetters, stringFilterGetters, boolFilterGetters, lowerBounds, higherBounds,
                    stringKeys, boolKeys)
                orderby sortGetter.Invoke(user, null)
                select user;
            return new ObservableCollection<Person>(sortedAndFiltered);
        }
        
        internal static ObservableCollection<Person> SortAndFilterUsers(IEnumerable<Person> users,
            List<MethodInfo> boundFilterGetters, List<MethodInfo> stringFilterGetters, 
            List<MethodInfo> boolFilterGetters, List<IComparable> lowerBounds, List<IComparable> higherBounds,
            List<string> stringKeys, List<bool?> boolKeys)
        {
            var sortedAndFiltered = from user in users
                where Match(user, boundFilterGetters, stringFilterGetters, boolFilterGetters, lowerBounds, higherBounds,
                    stringKeys, boolKeys)
                select user;
            return new ObservableCollection<Person>(sortedAndFiltered);
        }

        private static bool Match(Person user, List<MethodInfo> boundFilterGetters, 
            List<MethodInfo> stringFilterGetters, List<MethodInfo> boolFilterGetters, List<IComparable> lowerBounds, 
            List<IComparable> higherBounds, List<string> stringKeys, List<bool?> boolKeys)
        {
            var len = boundFilterGetters.Count;
            for (var i = 0; i < len; ++i)
            {
                var filterPropertyValue = (IComparable) boundFilterGetters[i].Invoke(user, null);
                if (lowerBounds[i] != null && filterPropertyValue.CompareTo(lowerBounds[i]) < 0 ||
                    higherBounds[i] != null && filterPropertyValue.CompareTo(higherBounds[i]) > 0)
                    return false;
            }

            len = stringFilterGetters.Count;
            for (var i = 0; i < len; ++i)
            {
                var filterPropertyValue = (string) stringFilterGetters[i].Invoke(user, null);
                if (stringKeys[i] != null && ! filterPropertyValue.Contains(stringKeys[i]))
                    return false;
            }
            
            len = boolFilterGetters.Count;
            for (var i = 0; i < len; ++i)
            {
                var filterPropertyValue = (bool) boolFilterGetters[i].Invoke(user, null);
                if (boolKeys[i] != null && ! filterPropertyValue == boolKeys[i])
                    return false;
            }
            
            return true;
        }
    }
}