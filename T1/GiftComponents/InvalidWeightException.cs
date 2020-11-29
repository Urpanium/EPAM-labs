using System;

namespace T1
{
    public class InvalidWeightException: Exception
    {
        public InvalidWeightException() : base($"Weight must be greater than 0")
        {
            
        }
    }
}