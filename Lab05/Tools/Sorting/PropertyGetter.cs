using System.Reflection;

namespace Lab05.Tools.Sorting
{
    public class PropertyGetter
    {
        public PropertyGetter(MethodInfo getter, string name)
        {
            Getter = getter;
            Name = name;
        }

        internal MethodInfo Getter { get; }
        
        internal string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}