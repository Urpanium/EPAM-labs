namespace T3.EventArgs
{
    public class OnConnectionChangedEventArgs: System.EventArgs
    {
        public readonly bool IsNowConnected;
        public readonly Terminal Terminal;

        public OnConnectionChangedEventArgs(bool isNowConnected, Terminal terminal)
        {
            IsNowConnected = isNowConnected;
            Terminal = terminal;
        }
    }
}