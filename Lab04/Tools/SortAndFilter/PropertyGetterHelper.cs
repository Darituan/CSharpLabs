using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Lab04.Tools.SortAndFilter
{
    internal static class PropertyGetterHelper
    {
        internal static ObservableCollection<PropertyGetter> GetPropertyGetters(Type type)
        {
            var getters = new ObservableCollection<PropertyGetter>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var getter = property.GetGetMethod();
                var sb = new StringBuilder();
                var propName = getter.Name.Replace("get_", "");
                if (propName == "EMail")
                    getters.Add(new PropertyGetter(getter, "E-Mail"));
                else
                {
                    sb.Append(propName[0]);
                    for (var i = 1; i < propName.Length; ++i)
                    {
                        if (char.IsUpper(propName, i))
                            sb.Append(' ');
                        sb.Append(propName[i]);
                    }
                    getters.Add(new PropertyGetter(getter, sb.ToString()));
                }
            }
            return getters;
        }
    }
}