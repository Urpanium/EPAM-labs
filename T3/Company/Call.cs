using System;

namespace T3
{
    public class Call
    {
        public readonly Client Caller;
        public readonly Client Target;
        public DateTime DateTime;
        public readonly float Length;

        public Call(Client caller, Client target, DateTime dateTime, float length)
        {
            Caller = caller;
            
            Target = target;
            DateTime = dateTime;
            Length = length;
        }
    }
}