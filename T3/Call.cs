namespace T3
{
    public class Call
    {
        
        public readonly Client Caller;
        public readonly Client Target;
        public readonly float Length;
        
        public Call(Client caller, Client target, float length)
        {
            Caller = caller;
            Target = target;
            Length = length;
        }
    }
}