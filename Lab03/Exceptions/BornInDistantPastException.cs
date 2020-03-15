using System;

namespace Lab03.Exceptions
{
    internal class BornInDistantPastException: ArgumentException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like You were born over 135 years ago. If this is a mistake, please try again." +
            " But if not, You should definitely go sign a lifespan record!";
        internal BornInDistantPastException()
        : base(DefaultMessage)
        {
            
        }
        
        
        internal BornInDistantPastException(string message)
            : base(message)
        {
            
        }
    }
}