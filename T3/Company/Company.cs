using System;
using System.Collections.Generic;
using System.Linq;
using T3.EventArgs;

namespace T3
{
    public class Company
    {
        public List<Station> Stations { get; }
        public List<Call> Calls { get; }
        public List<Client> Clients { get; }

        public Company(IEnumerable<Station> stations, IEnumerable<Client> clients)
        {
            Stations = stations.ToList();
            Clients = clients.ToList();
            Calls = new List<Call>();
            foreach (var station in Stations)
            {
                station.OnCallOccuredEvent += OnCallOccured;
            }
        }

        public int GetBillForClient(Client client)
        {
            int sum = 0;
            var clientCalls = from c in Calls
                where c.Caller.Equals(client) || c.Target.Equals(client)
                select c;

            foreach (var call in clientCalls)
                sum += (int) Math.Ceiling(call.Length * client.Tariff.MoneyPerCallMinute);

            return sum;
        }

        public Client FindClientByPortNumber(Station station, int portNumber)
        {
            var correspondingTerminalsEnumerable =
                from port in station.Ports
                where port.Number == portNumber
                select port.ConnectedTerminal;
            List<Terminal> correspondingTerminalsList = correspondingTerminalsEnumerable.ToList();

            if (correspondingTerminalsList.Count > 1)
                throw new Exception("why");
            if (correspondingTerminalsList.Count < 1)
                throw new Exception("No terminals found for this port");

            Terminal terminal = correspondingTerminalsList[0];
            var correspondingClientEnumerable = from client in Clients
                where client.Terminal.Equals(terminal)
                select client;
            List<Client> correspondingClientList = correspondingClientEnumerable.ToList();

            if (correspondingClientList.Count > 1)
                throw new Exception("Two or more clients found for this terminal! (WTF)");
            if (correspondingClientList.Count < 1)
                throw new Exception("No clients found for this terminal!");

            return correspondingClientList[0];
        }

        private void OnCallOccured(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            
            Client caller = FindClientByPortNumber(args.Station, args.CallerPortNumber);
            Client target = FindClientByPortNumber(args.Station, args.TargetPortNumber);
            Random random = new Random();
            Call call = new Call(caller, target, (float) random.NextDouble());
            Console.WriteLine($"Company: OnCallOccured, Length: {call.Length}");
            Calls.Add(call);
        }
    }
}