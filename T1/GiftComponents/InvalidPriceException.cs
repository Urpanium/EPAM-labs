using System;

namespace T1
{
    public class InvalidPriceException: Exception
    {
        public InvalidPriceException() : base($"Price must be greater or equal to 0")
        {
            
        }
    }
}