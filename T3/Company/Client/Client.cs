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
            if (Desire == ClientDesire.Active)
            {
                int target = Terminal.Port.Station.GetRandomPortNumberExcept(Terminal.Port.Number);
                Terminal.OnCallEvent.Invoke(new OnCallEventArgs(Terminal.Port.Number, target));
            }
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
            if (args.CallerPortNumber == Terminal.Port.Number)
            {
                return;
            }

            switch (Desire)
            {
                case ClientDesire.Active:
                {
                    Terminal.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(Terminal.Port.Station,
                        args.CallerPortNumber,
                        args.TargetPortNumber, args.TargetPortNumber, CallRespond.Accepted));
                    break;
                }
                case ClientDesire.Awake:
                {
                    Terminal.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(Terminal.Port.Station,
                        args.CallerPortNumber,
                        args.TargetPortNumber, args.TargetPortNumber, CallRespond.Accepted));
                    break;
                }
                default:
                {
                    Terminal.OnCallRespondEvent.Invoke(new OnCallRespondEventArgs(Terminal.Port.Station,
                        args.CallerPortNumber,
                        args.TargetPortNumber, args.TargetPortNumber, CallRespond.Rejected));
                    break;
                }
            }
        }

        private void OnOutgoingCallRespond(System.EventArgs eventArgs)
        {
            OnCallRespondEventArgs args = (OnCallRespondEventArgs) eventArgs;
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