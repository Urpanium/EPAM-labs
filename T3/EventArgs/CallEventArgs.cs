namespace T3.EventArgs
{
    public class CallEventArgs: System.EventArgs
    {
        public readonly int CallerPortNumber;
        public readonly int TargetPortNumber;

        public CallEventArgs(int callerPortNumber, int targetPortNumber)
        {
            CallerPortNumber = callerPortNumber;
            TargetPortNumber = targetPortNumber;
        }
    }
}