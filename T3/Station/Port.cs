using T3.Enums;
using T3.EventArgs;

namespace T3
{
    public class Port
    { 
        public bool IsConnected { get; private set; }
        public Status Status { get; private set; }
        public int Number { get; }
        public Terminal ConnectedTerminal { get; private set; }

        public delegate void PortHandler(System.EventArgs eventArgs);

        // port connection
        public PortHandler OnConnectionChangedEvent { get; } // fires when phone connection changes

        //calls
        public PortHandler OnCallEvent { get; set; }
        public PortHandler OnCallRespondEvent { get; set; }

        public Port(int number)
        {
            Number = number;
            Status = Status.Free;
            IsConnected = false;
            
            OnConnectionChangedEvent += OnConnectionChanged;
            OnCallEvent += OnCall;
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

        public void OnCall(System.EventArgs eventArgs)
        {
            Status = Status.Busy;
            if (IsConnected)
            {
                ConnectedTerminal.OnCallEvent.Invoke(eventArgs);
            }
        }

        public void OnCallRespond(System.EventArgs eventArgs)
        {
            
        }
    }
}