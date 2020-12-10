using System;
using System.Text;
using System.Xml.Serialization;
using T1.Enums;

namespace T1.GiftComponents
{
    
    //
    
    
    
    public abstract class GiftItemComponent: IGiftable
    {
        

        

        
        /*public string Name { get; set; }
        
        public string Manufacturer { get; set; }

        public float Weight { get; set; }*/

        
        public PriceType priceType { get; set; }
        
        public PriceRoundingRule priceRoundingRule { get; set; }

        public float RawPrice { get; set; }

        public float CalculatedPrice
        {
            get
            {
                switch (priceType)
                {
                    case PriceType.PerKilo:
                    {
                        return RoundPrice(Weight * RawPrice);
                    }
                    default:
                    {
                        return RoundPrice(RawPrice);
                    }
                }
            }
        }


        
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
            stringBuilder.Append($"\nTotal Price: {CalculatedPrice}");
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