namespace Lab03.Exceptions
{
    internal class BornInFutureException : BirthDateException
    {
        private static readonly string DefaultMessage =
            "Oops! It seems like You were born in future. If this is a mistake, please try again." +
            " But if not, You should consider going back to future to prevent time paradoxes!";

        internal BornInFutureException()
            : base(null, DefaultMessage)
        {

        }

        internal BornInFutureException(string paramName)
            : base(paramName, DefaultMessage)
        {

        }

        internal BornInFutureException(string paramName, string message)
            : base(paramName, message)
        {

        }
    }
}