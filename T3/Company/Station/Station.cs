using System;
using System.Collections.Generic;
using System.Linq;
using T3.Enums;
using T3.EventArgs;

namespace T3
{
    public class Station
    {
        static Station()
        {
            _random = new Random();
        }
        
        public Station(IEnumerable<Port> ports)
        {
            Ports = ports.ToList();
        }

        public Station(int portsCount)
        {
            Ports = new List<Port>();
            for (int i = 0; i < portsCount; i++)
            {
                Port port = new Port(this, i + 1);

                port.OnCallEvent += OnPortCall;
                port.OnCallRespondEvent += OnPortCallRespond;

                Ports.Add(port);
            }
        }

        private static Random _random;
        public List<Port> Ports { get; }

        public delegate void StationHandler(System.EventArgs eventArgs);

        public StationHandler OnCallOccuredEvent;

        

        public int GetRandomPortNumberExcept(int portNumber)
        {
            
            int firstTry = _random.Next(Ports.Count);
            if (Ports[firstTry].Number == portNumber)
                return (firstTry + _random.Next(Ports.Count - 1)) % Ports.Count;
            return firstTry;
        }


        //if terminal can be connected to any port of any station
        public void ConnectTerminal(Terminal terminal, Port port)
        {
            if (port.IsConnected)
                throw new Exception("Port is already connected!");
            OnConnectionChangedEventArgs args = new OnConnectionChangedEventArgs(true, terminal, port);
            port.OnConnectionChangedEvent.Invoke(args);
            terminal.OnConnectionChangedEvent.Invoke(args);
        }

        public void DisconnectTerminal(Terminal terminal)
        {
            IEnumerable<Port> correspondingPortEnumerable = from p in Ports
                where p.ConnectedTerminal.Equals(terminal)
                select p;

            List<Port> correspondingPortList = correspondingPortEnumerable.ToList();
            if (correspondingPortList.Count > 1)
                throw new Exception("There are two or more ports with same connected terminal!");
            if (correspondingPortList.Count < 1)
                throw new Exception("No port was found with such terminal connected!");
            Port correspondingPort = correspondingPortList[0];
            if (!correspondingPort.IsConnected)
                throw new Exception("Port is already disconnected!");

            correspondingPort.OnConnectionChangedEvent.Invoke(
                new OnConnectionChangedEventArgs(false, terminal, correspondingPort));
            terminal.OnConnectionChangedEvent.Invoke(
                new OnConnectionChangedEventArgs(false, terminal, correspondingPort));
        }

        //when someone decides to make a call
        private void OnPortCall(System.EventArgs eventArgs)
        {
            OnCallEventArgs args = (OnCallEventArgs) eventArgs;
            Port caller = FindPortByPortNumber(args.CallerPortNumber);
            if (IsPortWithNumberConnected(args.TargetPortNumber))
            {
                Port target = FindPortByPortNumber(args.TargetPortNumber);
                /*if (!target.IsConnected)
                {
                    caller.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                        args.TargetPortNumber, CallRespond.Disconnected));
                    return;
                }*/

                // looks ugly, but it works
                target.OnCallEvent -= OnPortCall;
                target.OnCallEvent.Invoke(args);
                target.OnCallEvent += OnPortCall;
            }
            else
            {
                
                caller.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(this, args.CallerPortNumber,
                    args.TargetPortNumber, args.TargetPortNumber, CallRespond.Disconnected));
            }
        }

        private void OnPortCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            if (args.ResponderPortNumber == args.TargetPortNumber)
            {
                Port caller = FindPortByPortNumber(args.CallerPortNumber);
                // pass the information to port

                // caller.OnCallRespondEvent -= OnPortCallRespond;
                caller.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(this, args.CallerPortNumber,
                    args.TargetPortNumber, args.CallerPortNumber, args.CallRespond));
                // caller.OnCallRespondEvent += OnPortCallRespond;

                // if call is accepted
                if (args.CallRespond == CallRespond.Accepted)
                {
                    // add this call to company statistics using the event
                    OnCallOccuredEvent.Invoke(eventArgs);
                }
            }

            
        }

        private bool IsPortWithNumberConnected(int portNumber)
        {
            var correspondingPortEnumerable = from p in Ports
                where p.Number == portNumber
                select p;
            List<Port> correspondingPortList = correspondingPortEnumerable.ToList();
            return correspondingPortList.Count >= 1;
        }

        private Port FindPortByPortNumber(int portNumber)
        {
            var correspondingPortEnumerable = from p in Ports
                where p.Number == portNumber
                select p;
            List<Port> correspondingPortList = correspondingPortEnumerable.ToList();
            if (correspondingPortList.Count > 1)
                throw new Exception("There are two or more ports with same connected terminal!");
            if (correspondingPortList.Count < 1)
                return null;
            return correspondingPortList[0];
        }
    }
}