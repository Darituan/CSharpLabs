using System;

namespace Lab04.Tools.SortAndFilter
{
    internal static class Functor
    {
        internal static Func<T, T, bool> Greater<T>()
            where T : IComparable<T>
        {
            return (lhs, rhs) => lhs.CompareTo(rhs) > 0;
        }

        internal static Func<T, T, bool> Less<T>()
            where T : IComparable<T>
        {
            return (lhs, rhs) => lhs.CompareTo(rhs) < 0;
        }
        
        internal static Func<T, T, bool> GreaterEquals<T>()
            where T : IComparable<T>
        {
            return (lhs, rhs) => lhs.CompareTo(rhs) >= 0;
        }

        internal static Func<T, T, bool> LessEquals<T>()
            where T : IComparable<T>
        {
            return (lhs, rhs) => lhs.CompareTo(rhs) <= 0;
        }
        
        internal static Func<T, T, bool> Equals<T>()
            where T : IComparable<T>
        {
            return (lhs, rhs) => lhs.Equals(rhs);
        }
        
        
    }
}