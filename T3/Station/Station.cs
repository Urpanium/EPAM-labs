using System;
using System.Collections.Generic;
using System.Linq;
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
                Ports.Add(port);
            }
        }


        //if terminal can be connected only to specific port
        public void ConnectTerminal(Terminal terminal)
        {
            IEnumerable<Port> correspondingPortEnumerable = from p in Ports
                where p.Number == terminal.PortNumber
                select p;
            List<Port> correspondingPortList = correspondingPortEnumerable.ToList();
            if (correspondingPortList.Count > 1)
                throw new Exception("There are two or more ports with same number!");
            if (correspondingPortList.Count < 1)
                throw new Exception("No port was found with such number!");
            Port correspondingPort = correspondingPortList[0];
            if (correspondingPort.IsConnected)
                throw new Exception("Port is already connected!");

            correspondingPort.OnTerminalConnectionChangedEvent.Invoke(new OnConnectionChangedEventArgs(true, terminal));
        }

        //if terminal can be connected to any port of any station
        public void ConnectTerminal(Terminal terminal, Port port)
        {
            if (port.IsConnected)
                throw new Exception("Port is already connected!");
            port.OnTerminalConnectionChangedEvent.Invoke(new OnConnectionChangedEventArgs(true, terminal));
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

            correspondingPort.OnTerminalConnectionChangedEvent(new OnConnectionChangedEventArgs(false, terminal));
        }
    }
}