using System;
using System.Linq;
using System.Windows.Markup;

namespace Lab04.Enums
{
    public class EnumBindingSource : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return _enumType; }
            set
            {
                if (value != _enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    _enumType = value;
                }
            }
        }

        public EnumBindingSource() { }

        public EnumBindingSource(Type enumType)
        {
            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(_enumType)
                .Cast<object>()
                .Select(e => new { Value = e, DisplayName = EnumDescriptionHelper.GetDescription((Enum)e) });
        }
    }
}