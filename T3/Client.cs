using System;
using T3.Enums;
using T3.EventArgs;

namespace T3
{
    public class Client
    {
        public static Random Random;
        public Terminal Terminal { get; }

        public Tariff Tariff { get; }
        public ClientDesire Desire { get; private set; }

        static Client()
        {
            Random = new Random(0);
        }

        public Client(Terminal terminal, Tariff tariff)
        {
            Terminal = terminal;
            Tariff = tariff;

            Terminal.OnCallEvent += OnCall;
            Terminal.OnCallRespondEvent += OnOutgoingCallRespond;
        }


        public void Live()
        {
            Think();
            Console.WriteLine($"State: {Desire} ");
            if (Desire == ClientDesire.Active)
            {
                //TODO: make call
                int target = Terminal.Port.Station.GetRandomPortNumberExcept(Terminal.Port.Number);
                Console.WriteLine($"Calling {target}");
                Terminal.OnCallEvent.Invoke(new OnCallEventArgs(Terminal.Port.Station, Terminal.Port.Number, target));
            }

            Console.WriteLine();
        }

        private void Think()
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

        private void OnCall(System.EventArgs eventArgs)
        {
            OnCallEventArgs args = (OnCallEventArgs) eventArgs;
            //if someone calls us
            if (args.CallerPortNumber != Terminal.Port.Number)
            {
                switch (Desire)
                {
                    case ClientDesire.Active:
                    {
                        Terminal.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                            args.TargetPortNumber, args.TargetPortNumber, CallRespond.Accepted));
                        Console.WriteLine();
                        break;
                    }
                    case ClientDesire.Awake:
                    {
                        Terminal.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                            args.TargetPortNumber, args.TargetPortNumber, CallRespond.Accepted));
                        break;
                    }
                    default:
                    {
                        Terminal.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(args.CallerPortNumber,
                            args.TargetPortNumber, args.TargetPortNumber, CallRespond.Rejected));
                        break;
                    }
                }
            }
        }

        private void OnOutgoingCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
            Console.WriteLine(
                $"Client: OnCallRespondEvent, CallRespond: {args.CallRespond}, Caller: {args.CallerPortNumber}, Target: {args.TargetPortNumber}");
            CallRespond respond = args.CallRespond;

            //if it was our call
            if (args.CallerPortNumber == Terminal.Port.Number)
            {
                if (respond == CallRespond.Accepted)
                {
                    Desire = ClientDesire.Talking;
                }
            }
        }
    }
}