using T3.Enums;

namespace T3.EventArgs
{
    public class OnCallRespondEventArgs : System.EventArgs
    {
        public readonly int CallerPortNumber;
        public readonly int TargetPortNumber;
        public readonly CallRespond CallRespond;

        public OnCallRespondEventArgs(int callerPortNumber, int targetPortNumber, CallRespond callRespond)
        {
            CallerPortNumber = callerPortNumber;
            TargetPortNumber = targetPortNumber;
            CallRespond = callRespond;
        }
    }
}