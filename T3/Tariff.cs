namespace T3
{
    public class Tariff
    {
        public Tariff(int moneyPerCallMinute, int moneyPerConnectionMinute)
        {
            MoneyPerCallMinute = moneyPerCallMinute;
            MoneyPerConnectionMinute = moneyPerConnectionMinute;
        }
        public int MoneyPerCallMinute { get;}
        public int MoneyPerConnectionMinute { get; }
    }
}