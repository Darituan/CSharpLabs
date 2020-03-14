using System;

namespace Lab03.Exceptions
{
    internal class BirthDateException: ArgumentOutOfRangeException
    {
        internal BirthDateException(){}

        internal BirthDateException(string paramName)
            : base(paramName)
        {
            
        }
        
        internal BirthDateException(string paramName, string message)
            : base(paramName, message)
        {
            
        }
    }
}