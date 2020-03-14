using System;

namespace Lab03.Exceptions
{
    internal class InvalidEmailException: ArgumentException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like there is a mistake in Your e-mail address";

        internal InvalidEmailException()
            : base(DefaultMessage)
        {
            
        }
        
        internal InvalidEmailException(string message)
            : base(message)
        {
            
        }
    }
}