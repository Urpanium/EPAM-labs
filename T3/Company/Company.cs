using System;
using System.Collections.Generic;
using System.Linq;
using T3.EventArgs;

namespace T3
{
    public class Company
    {
        private static DateTime _startDateTime;
        private static Random _random;

        static Company()
        {
            _startDateTime = DateTime.Now;
            _random = new Random();
        }

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

        public List<Station> Stations { get; }
        public List<Call> Calls { get; }
        public List<Client> Clients { get; }


        
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
        
        public int GetBillForClient(Client client, DateTime fromTime, DateTime toTime)
        {
            int sum = 0;
            var clientCalls = from c in Calls
                where (c.Caller.Equals(client) || c.Target.Equals(client)) && 
                      DateTime.Compare(c.DateTime, toTime) >= 0 && DateTime.Compare(c.DateTime, fromTime) <= 0
                select c;

            foreach (var call in clientCalls)
                sum += CalculateCallPrice(call, client.Tariff);

            return sum;
        }
        
        public IEnumerable<Call> GetCallsForClient(Client client, DateTime fromTime, DateTime toTime, float fromSum, float toSum)
        {
            var clientCalls = from c in Calls
                where (c.Caller.Equals(client) || c.Target.Equals(client)) &&
                      DateTime.Compare(c.DateTime, toTime) >= 0 && DateTime.Compare(c.DateTime, fromTime) <= 0 &&
                      CalculateCallPrice(c, client.Tariff) >= fromSum && CalculateCallPrice(c, client.Tariff) <= toSum
                select c;


            return clientCalls;
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
                return null;

            Terminal terminal = correspondingTerminalsList[0];
            var correspondingClientEnumerable = from client in Clients
                where client.Terminal.Equals(terminal)
                select client;
            List<Client> correspondingClientList = correspondingClientEnumerable.ToList();

            if (correspondingClientList.Count > 1)
                throw new Exception("Two or more clients found for this terminal! (WTF)");
            if (correspondingClientList.Count < 1)
                return null;

            return correspondingClientList[0];
        }

        private void OnCallOccured(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;

            Client caller = FindClientByPortNumber(args.Station, args.CallerPortNumber);
            Client target = FindClientByPortNumber(args.Station, args.TargetPortNumber);
            Int64 deltaTime = DateTime.Now.Ticks - _startDateTime.Ticks;

            Int64 now = Math.Min(deltaTime * 1000000, DateTime.MaxValue.Ticks);
            Call call = new Call(caller, target, new DateTime(now), (float) _random.NextDouble());
            Calls.Add(call);
        }

        private int CalculateCallPrice(Call call, Tariff tariff)
        {
            return (int) Math.Ceiling(call.Length * tariff.MoneyPerCallMinute);
        }
    }
}