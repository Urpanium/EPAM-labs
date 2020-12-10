using System;
using System.Collections.Generic;
using T1.Enums;

namespace T1
{
    //это что бля?
    public abstract class GiftStuffBase
    {
        public GiftStuffBase()
        {
            
        }

        public string Name { get; set; }
        public string Manufacturer { get; set; }

        public List<GiftStuffBase> Components;
        
        /*public float GetWeight { get; set; }*/

        /*public PriceType priceType { get; set; }

        public PriceRoundingRule priceRoundingRule { get; set; }

        public float RawPrice { get; set; }*/
        
        /*public float CalculatedPrice
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
        }*/
        //override in children
        public float GetWeight()
        {
            float sum = 0.0f;
            foreach (GiftStuffBase component in Components)
            {
                sum += component.GetWeight();
            }

            return sum;
        }

        //override in children
        public float GetPrice()
        {
            float sum = 0.0f;
            foreach (GiftStuffBase component in Components)
            {
                sum += component.GetPrice();
            }

            return sum;
        }
    }
}