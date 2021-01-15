using System;
using System.Collections.Generic;

namespace T3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Tariff tariff = new Tariff(10);
            Company company = MakeCompany(10, 50, tariff);
            int iterations = 1000;
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"Iteration #{i + 1}");
                for (int j = 0; j < company.Clients.Count; j++)
                {
                    Client client = company.Clients[j];
                    
                    client.Live();
                }
            }
            
            ShowCalls(company);
            // GetBillForClient(Client)
        }

        static Company MakeCompany(int stationsCount, int clientsCount, Tariff tariff)
        {
            Random random = new Random(0);
            List<Station> stations = new List<Station>();

            for (int i = 0; i < stationsCount; i++)
            {
                Station station = new Station(clientsCount);
                stations.Add(station);
            }

            List<Client> clients = new List<Client>();

            for (int i = 0; i < clientsCount; i++)
            {
                Terminal clientTerminal = new Terminal(i + 1);

                int stationIndex = random.Next(stationsCount);
                stations[stationIndex].ConnectTerminal(clientTerminal, stations[stationIndex].Ports[i]);

                Client client = new Client(clientTerminal, tariff);
                clients.Add(client);
            }


            Company company = new Company(stations, clients);
            return company;
        }

        static void ShowCalls(Company company)
        {
            for (int i = 0; i < company.Calls.Count; i++)
            {
                Call call = company.Calls[i];
                Console.WriteLine($"Call #{i + 1}: Length = {call.Length}, Time = {call.DateTime}, Target = {call.Target.Terminal.Port.Number}, Caller = {call.Caller.Terminal.Port.Number}");
            }
        }
    }
}