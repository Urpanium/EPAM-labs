using System;
using System.Collections.Generic;
using System.Linq;
using T3.Enums;
using T3.EventArgs;

namespace T3
{
    public class Station
    {
        public List<Port> Ports { get; }

        public delegate void StationHandler(System.EventArgs eventArgs);

        public StationHandler OnCallOccuredEvent;

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

        public int GetRandomPortNumberExcept(int portNumber)
        {
            Random random = new Random();
            int firstTry = random.Next(Ports.Count);
            if (Ports[firstTry].Number == portNumber)
                return (firstTry + random.Next(Ports.Count - 1)) % Ports.Count;
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
            Console.WriteLine(
                $"Station: OnCallEvent, Target: {args.TargetPortNumber}, Caller: {args.CallerPortNumber}");
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
                //target.OnCallEvent -= OnPortCall;
                target.OnCallEvent.Invoke(args);
                //target.OnCallEvent += OnPortCall;
            }
            else
            {
                Console.WriteLine("Station: No such port with connected terminal was found");
                caller.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                    args.TargetPortNumber, args.TargetPortNumber, CallRespond.Disconnected));
            }
        }

        private void OnPortCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            Console.WriteLine(
                $"Station: OnCallRespondEvent, CallRespond: {args.CallRespond}, Caller: {args.CallerPortNumber}, Target: {args.TargetPortNumber}, Responded: {args.ResponderPortNumber}");
            if (args.ResponderPortNumber == args.TargetPortNumber)
            {
                /*int sendTo = args.ResponderPortNumber == args.CallerPortNumber
                    ? args.TargetPortNumber
                    : args.CallerPortNumber;*/
                Port caller = FindPortByPortNumber(args.CallerPortNumber);
                // pass the information to port

                caller.OnCallEvent -= OnPortCallRespond;
                caller.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                    args.TargetPortNumber, args.CallerPortNumber, args.CallRespond));
                caller.OnCallEvent += OnPortCallRespond;

                // if call is accepted
                if (args.CallRespond == CallRespond.Accepted)
                    // add this call to company statistics using the event
                    OnCallOccuredEvent.Invoke(eventArgs);
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
                throw new Exception("No port was found with such terminal connected!");
            return correspondingPortList[0];
        }
    }
}