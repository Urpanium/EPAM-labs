using System;
using System.Collections.Generic;

namespace T3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Tariff tariff = new Tariff(10);
            
        }

        Company MakeCompany(int stationsCount, int clientsCount, Tariff tariff)
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
                Client client = new Client(clientTerminal, tariff);
                clients.Add(client);
            }
            


            Company company = new Company(stations, clients);
            return company;
        }
    }
}