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
        internal static ObservableCollection<Person> SortedUsers(IEnumerable<Person> users, 
            MethodInfo propGetter)
        {
            return new ObservableCollection<Person>
               ( from user in users
                orderby propGetter.Invoke(user, null)
                select user );
        }

        internal static ObservableCollection<Person> FilteredUsers<T>(IEnumerable<Person> users,
            MethodInfo propGetter, Func<T, T, bool> keyFunc, T key)
        where T: IComparable<T>
        {
            return new ObservableCollection<Person>
            ( from user in users
                where keyFunc((T)propGetter.Invoke(user, null), key)
                select user );
        }
        
        internal static ObservableCollection<Person> FilteredUsers(IEnumerable<Person> users,
            MethodInfo propGetter, bool propIsTrue)
        {
            return new ObservableCollection<Person>
            ( from user in users
                where propIsTrue ? (bool)propGetter.Invoke(user, null) : 
                    ! (bool)propGetter.Invoke(user, null)
                select user );
        }
    }
}