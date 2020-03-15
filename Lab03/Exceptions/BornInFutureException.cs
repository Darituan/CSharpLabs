using System;

namespace Lab03.Exceptions
{
    internal class BornInFutureException : ArgumentException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like You were born in future. If this is a mistake, please try again." +
            " But if not, You should consider going back to future to prevent time paradoxes!";

        internal BornInFutureException()
            : base(DefaultMessage)
        {

        }

        internal BornInFutureException(string message)
            : base(message)
        {

        }
    }
}