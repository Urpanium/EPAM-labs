using System;
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
            OnCallRespondEvent += OnCallRespond;
        }


        private void OnCall(System.EventArgs eventArgs)
        {
            OnCallEventArgs args = (OnCallEventArgs) eventArgs;
            Console.WriteLine(
                $"Terminal: OnCallEvent, Target: {args.TargetPortNumber}, Caller: {args.CallerPortNumber}");
            // if it is our call
            if (args.CallerPortNumber == Port.Number)
            {
                if (!IsConnected)
                    throw new Exception("Can't make call because terminal is not connected!");

                Port.OnCallEvent.Invoke(args);
            }
        }

        private void OnCallRespond(System.EventArgs eventArgs)
        {
            if (!IsConnected)
                throw new Exception("wtf");

            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            Console.WriteLine(
                $"Terminal: OnCallRespondEvent, CallRespond: {args.CallRespond}, Caller: {args.CallerPortNumber}, Target: {args.TargetPortNumber}");

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