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

        public Station(IEnumerable<Port> ports)
        {
            Ports = ports.ToList();
        }

        public Station(int portsCount)
        {
            for (int i = 0; i < portsCount; i++)
            {
                Port port = new Port(i + 1);
                
                port.OnCallEvent += OnPortCall;
                port.OnCallRespondEvent += OnPortCallRespond;
                
                Ports.Add(port);
            }
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

            target.OnCallEvent.Invoke(new OnCallEventArgs(caller.Number, target.Number));
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
        
        
        private void OnPortCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            CallRespond respond = args.CallRespond;
            Port caller = FindPortByPortNumber(args.CallerPortNumber);
            //pass the information to port
            caller.OnCallRespondEvent.Invoke(eventArgs);
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