using System;

namespace Lab04.Exceptions
{
    public class InvalidSurnameException: ArgumentException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like there is a mistake in Your surname";

        internal InvalidSurnameException()
            : base(DefaultMessage)
        {

        }

        internal InvalidSurnameException(string message)
            : base(message)
        {

        }
    }
}