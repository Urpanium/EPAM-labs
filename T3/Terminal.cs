namespace T3
{
    public class Terminal
    {
        public delegate void TerminalHandler(System.EventArgs eventArgs);

        public event TerminalHandler OnConnectionChangedEvent;
        public event TerminalHandler OnCallEvent;
        
        public int PortNumber { get; }
        public int Number { get; }
        public bool IsConnected { get; }
        
        
    }
}