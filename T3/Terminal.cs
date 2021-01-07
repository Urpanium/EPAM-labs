using T3.EventArgs;

namespace T3
{
    public class Terminal
    {
        public delegate void TerminalHandler(System.EventArgs eventArgs);

        public TerminalHandler OnConnectionChangedEvent { get; }

        public TerminalHandler OnCallEvent;

        public TerminalHandler OnCallRespondEvent;

        public bool IsConnected { get; private set; }

        public Port Port { get; set; }

        public int Number { get; }

        public Terminal(int number)
        {
            Number = number;
            OnConnectionChangedEvent += OnConnectionChanged;
            OnCallEvent += OnCall;

        }

        

        private void OnCall(System.EventArgs eventArgs)
        {
            OnCallEventArgs args = (OnCallEventArgs) eventArgs;
            // why
        }

        private void OnConnectionChanged(System.EventArgs eventArgs)
        {
            OnConnectionChangedEventArgs args = (OnConnectionChangedEventArgs) eventArgs;
            IsConnected = args.IsNowConnected;
            if (IsConnected)
                Port = args.Port;
            else
                Port = null;
        }
    }
}