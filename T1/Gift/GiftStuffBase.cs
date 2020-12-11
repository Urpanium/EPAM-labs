using System.Collections.Generic;

namespace T1.Gift
{
    
    public abstract class GiftStuffBase
    {
        public GiftStuffBase()
        {
            Components = new List<GiftStuffBase>();
        }

        public string Name { get; set; }
        public string Manufacturer { get; set; }

        public List<GiftStuffBase> Components { get; set; }
        
        
        public float GetWeight()
        {
            float sum = 0.0f;
            foreach (GiftStuffBase component in Components)
            {
                sum += component.GetWeight();
            }

            return sum;
        }

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