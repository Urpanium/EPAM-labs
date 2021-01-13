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
            OnCallRespondEvent += OnCallRespond;
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
            Console.WriteLine(
                $"Port {Number}: OnCallEvent, Target: {args.TargetPortNumber}, Caller: {args.CallerPortNumber}");

            // when call occurs
            // in every case port will become busy
            Status = Status.Busy;

            // if call is not initiated by us
            if (args.CallerPortNumber != Number)
            {
                // if target terminal is connected
                if (IsConnected)
                    ConnectedTerminal.OnCallEvent.Invoke(eventArgs);
                else
                    OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(Station, args.CallerPortNumber, args.TargetPortNumber,
                        args.TargetPortNumber, CallRespond.Disconnected));
            }
        }

        public void OnCallRespond(System.EventArgs eventArgs)
        {
            Status = Status.Free;
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            Console.WriteLine(
                $"Port {Number}: OnCallRespondEvent, CallRespond: {args.CallRespond}, Target: {args.TargetPortNumber}, Caller: {args.CallerPortNumber}");

            // if someone responded to us (so this port number will be different)
            if (args.CallerPortNumber == Number)
            {
                if (!IsConnected)
                    throw new Exception("No terminal to send respond!");
                ConnectedTerminal.OnCallRespondEvent.Invoke(args);
            }
            else
            {
                //send to station?
            }
            // do nothing if it's our respond to call 
        }
    }
}