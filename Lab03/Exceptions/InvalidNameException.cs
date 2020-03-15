using System;

namespace Lab03.Exceptions
{
    public class InvalidNameException: ArgumentException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like there is a mistake in Your name";

        internal InvalidNameException()
            : base(DefaultMessage)
        {

        }

        internal InvalidNameException(string message)
            : base(message)
        {

        }
    }
}