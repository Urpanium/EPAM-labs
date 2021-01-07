namespace T3.EventArgs
{
    public class OnCallEventArgs: System.EventArgs
    {
        public readonly int CallerPortNumber;
        public readonly int TargetPortNumber;

        public OnCallEventArgs(int callerPortNumber, int targetPortNumber)
        {
            CallerPortNumber = callerPortNumber;
            TargetPortNumber = targetPortNumber;
        }
    }
}