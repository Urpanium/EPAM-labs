using T3.Enums;
using T3.EventArgs;

namespace T3
{
    public class Port
    {
        //public delegate void PortCallHandler
        public bool IsConnected { get; private set; }
        public Status Status { get; }
        public int Number { get; }
        public Terminal ConnectedTerminal { get; private set; }

        public delegate void PortHandler(System.EventArgs eventArgs);

        // port connection
        public PortHandler OnTerminalConnectionChangedEvent { get; } // fires when phone connection changes

        // incoming calls
        public PortHandler OnIncomingCallEvent { get; } // Mike wants to talk about a tough nut
        public PortHandler OnIncomingCallAcceptedEvent { get; } // we wanna talk about a tough nut too
        public PortHandler OnIncomingCallDeclinedEvent { get; } // fuck you, Mike

        //outgoing calls
        public PortHandler OnCallEvent;
        public PortHandler OnCallRejectedEvent;

        public Port(int number)
        {
            Number = number;
            Status = Status.Free;
            IsConnected = false;
            OnTerminalConnectionChangedEvent += OnConnectionChanged;
        }

        public void OnConnectionChanged(System.EventArgs eventArgs)
        {
            OnConnectionChangedEventArgs args = (OnConnectionChangedEventArgs) eventArgs;
            IsConnected = args.IsNowConnected;
            if (IsConnected)
                ConnectedTerminal = args.Terminal;
            else
                ConnectedTerminal = null;
        }
    }
}