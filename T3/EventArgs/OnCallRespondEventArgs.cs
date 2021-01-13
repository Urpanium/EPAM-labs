using T3.Enums;

namespace T3.EventArgs
{
    public class OnCallRespondEventArgs : System.EventArgs
    {
        public readonly Station Station;
        public readonly int CallerPortNumber;
        public readonly int TargetPortNumber;
        public readonly int ResponderPortNumber;
        public readonly CallRespond CallRespond;

        public OnCallRespondEventArgs(Station station, int callerPortNumber, int targetPortNumber, int responderPortNumber,
            CallRespond callRespond)
        {
            Station = station;
            CallerPortNumber = callerPortNumber;
            TargetPortNumber = targetPortNumber;
            ResponderPortNumber = responderPortNumber;
            CallRespond = callRespond;
        }
    }
}