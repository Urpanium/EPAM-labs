using T3.Enums;

namespace T3.EventArgs
{
    public class CallRespondEventArgs: System.EventArgs
    {
        public readonly CallRespond CallRespond;

        public CallRespondEventArgs(CallRespond callRespond)
        {
            CallRespond = callRespond;
        }
    }
}