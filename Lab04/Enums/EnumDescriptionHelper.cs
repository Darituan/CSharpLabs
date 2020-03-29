using System;
using System.ComponentModel;
using System.Linq;

namespace Lab04.Enums
{
    internal static class EnumDescriptionHelper
    {
        internal static string GetDescription<T>(T genericEnum) where T: Enum
        {
            var memberInfo = typeof(T).GetMember(genericEnum.ToString());
            if (memberInfo.Length <= 0) return genericEnum.ToString();
            var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribs.Any() ? ((DescriptionAttribute)attribs.ElementAt(0)).Description : genericEnum.ToString();
        }
    }
}