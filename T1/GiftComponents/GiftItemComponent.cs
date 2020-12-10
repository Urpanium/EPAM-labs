using System;
using System.Text;
using T1.Enums;

namespace T1.GiftComponents
{
    public abstract class GiftItemComponent : GiftStuffBase
    {
        public GiftItemComponent()
        {
        }

        public GiftItemComponent(string name, string manufacturer, float weight, PriceType priceType,
            PriceRoundingRule priceRoundingRule)
        {
            Name = name;
            Manufacturer = manufacturer;
            Weight = weight;
            this.priceType = priceType;
            this.priceRoundingRule = priceRoundingRule;
        }

        public PriceType priceType { get; set; }

        public PriceRoundingRule priceRoundingRule { get; set; }

        public float RawPrice { get; set; }

        public float Weight { get; set; }

        public new float GetPrice()
        {
            switch (priceType)
            {
                case PriceType.PerKilo:
                {
                    return RoundPrice(GetWeight() * RawPrice);
                }
                default:
                {
                    return RoundPrice(RawPrice);
                }
            }
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetInfoString());
            stringBuilder.Append(GetPriceString());
            return stringBuilder.ToString();
        }

        public string GetInfoString()
        {
            return $"{Name}, manufactured by {Manufacturer} \nWeight: {Weight} \nPrice Type: {priceType} \n";
        }

        protected string GetPriceString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append((priceType == PriceType.PerKilo
                ? $"Price Per Kilo: {RawPrice}"
                : ""));
            stringBuilder.Append($"\nTotal Price: {GetPrice()}");
            return stringBuilder.ToString();
        }


        private float RoundPrice(float floatPrice)
        {
            switch (priceRoundingRule)
            {
                case PriceRoundingRule.Floor:
                {
                    return (float) Math.Floor(floatPrice * 100) / 100.0f;
                }
                default:
                {
                    return (float) Math.Round(floatPrice * 100) / 100.0f;
                }
            }
        }
    }
}