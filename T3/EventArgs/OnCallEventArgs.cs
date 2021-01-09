namespace T3.EventArgs
{
    public class OnCallEventArgs: System.EventArgs
    {
        public Station Station;
        public readonly int CallerPortNumber;
        public readonly int TargetPortNumber;

        public OnCallEventArgs(Station station, int callerPortNumber, int targetPortNumber)
        {
            Station = station;
            CallerPortNumber = callerPortNumber;
            TargetPortNumber = targetPortNumber;
        }
    }
}