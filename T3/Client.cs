using System;
using System.Diagnostics;
using T3.Enums;

namespace T3
{
    public class Client
    {
        public static Random Random;

        public string Name { get; } // bring some personality to synthetic human
        public Terminal Terminal { get; }
        public ClientDesire Desire { get; private set; }

        static Client()
        {
            Random = new Random(0);
        }

        public void Live()
        {
            Think();
            switch (Desire)
            {
                case ClientDesire.Active:
                {
                    //TODO: make some call
                    break;
                }
                case ClientDesire.Awake:
                {
                    //TODO: accept calls
                    break;
                }
                case ClientDesire.Talking:
                {
                    //TODO: talk
                    break;
                }
                default:
                {
                    //TODO: i sleep
                    break;
                }
            }
        }

        public void Think()
        {
            int value = Random.Next(100);

            if (value < 50)
            {
                if (Desire != ClientDesire.Talking)
                    Desire = ClientDesire.Sleep;
                else 
                    Desire = ClientDesire.Talking;
                return;
            }

            if (value < 75)
            {
                Desire = ClientDesire.Awake;
                return;
            }

            Desire = ClientDesire.Active;
        }
    }
}