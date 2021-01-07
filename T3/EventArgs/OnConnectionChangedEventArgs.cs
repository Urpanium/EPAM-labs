namespace T3.EventArgs
{
    public class OnConnectionChangedEventArgs: System.EventArgs
    {
        public readonly bool IsNowConnected;
        public readonly Terminal Terminal;
        public readonly Port Port;

        public OnConnectionChangedEventArgs(bool isNowConnected, Terminal terminal, Port port)
        {
            IsNowConnected = isNowConnected;
            Terminal = terminal;
            Port = port;
        }
    }
}