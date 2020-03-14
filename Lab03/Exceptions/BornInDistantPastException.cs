﻿namespace Lab03.Exceptions
{
    internal class BornInDistantPastException: BirthDateException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like You were born over 135 years ago. If this is a mistake, please try again." +
            " But if not, You should definitely go sign a lifespan record!";
        internal BornInDistantPastException()
        : base(null, DefaultMessage)
        {
            
        }
        
        internal BornInDistantPastException(string paramName)
            : base(paramName, DefaultMessage)
        {
            
        }
        
        internal BornInDistantPastException(string paramName, string message)
            : base(paramName, message)
        {
            
        }
    }
}