using System;
using T3.Enums;
using T3.EventArgs;

namespace T3
{
    public class Port
    { 
        public bool IsConnected { get; private set; }
        public Status Status { get; private set; }
        
        public Station Station { get; }
        public int Number { get; } // port number
        public Terminal ConnectedTerminal { get; private set; }

        public delegate void PortHandler(System.EventArgs eventArgs);

        // port connection
        public PortHandler OnConnectionChangedEvent { get; } // fires when phone connection changes

        //calls
        public PortHandler OnCallEvent { get; set; }
        public PortHandler OnCallRespondEvent { get; set; }

        public Port(Station station, int number)
        {
            Number = number;
            Status = Status.Free;
            IsConnected = false;
            Station = station;
            
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
            OnCallEventArgs args = (OnCallEventArgs) eventArgs;
            // when call occurs
            // in every case port will become busy
            Status = Status.Busy;
            
            // if call is not initiated by us
            if (args.CallerPortNumber != Number)
            {
                // if target terminal is connected
                if (IsConnected)
                {
                    ConnectedTerminal.OnCallEvent.Invoke(eventArgs);
                }
            }
        }

        public void OnCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            // if someone responded to us (so this port number will be different)
            if (args.CallerPortNumber != Number)
            {
                if (!IsConnected)
                    throw new Exception("No terminal to send respond!");
                ConnectedTerminal.OnCallRespondEvent.Invoke(args);
            }
            // do nothing if it's our respond to call
        }
    }
}