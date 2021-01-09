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
            port.OnConnectionChangedEvent.Invoke(new OnConnectionChangedEventArgs(true, terminal, port));
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
            Port target = FindPortByPortNumber(args.TargetPortNumber);
            if (!target.IsConnected)
            {
                caller.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                    args.TargetPortNumber, CallRespond.Disconnected));
                return;
            }

            target.OnCallEvent.Invoke(new OnCallEventArgs(this, caller.Number, target.Number));
        }

        private void OnPortCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            Port caller = FindPortByPortNumber(args.CallerPortNumber);
            // pass the information to port
            caller.OnCallRespondEvent.Invoke(eventArgs);
            // if call is accepted
            if (args.CallRespond == CallRespond.Accepted)
                // add this call to company statistics using the event
                OnCallOccuredEvent.Invoke(eventArgs);
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