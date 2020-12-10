using System;
using System.Collections.Generic;

using T1.GiftComponents;

namespace T1
{
    public class Food : GiftItem
    {
        
        public new List<GiftItemComponent> Components;

        public new float Weight
        {
            get
            {
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += component.Weight;
                }

                return sum;
            }
        }

        public new float Price
        {
            get
            {
                
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += component.CalculatedPrice;
                }

                return sum;
            }
        }

        public float Sweetness
        {
            get
            {
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += Sweetness;
                }

                return sum / ComponentsCount;
            }
        }

        public float Sourness
        {
            get
            {
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += Sourness;
                }

                return sum / ComponentsCount;
            }
        }

        public float Bitterness
        {
            get
            {
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += Bitterness;
                }

                return sum / ComponentsCount;
            }
        }

        public float Salinity
        {
            get
            {
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += Salinity;
                }

                return sum / ComponentsCount;
            }
        }

        public new GiftItemComponent this[int index]
        {
            get
            {
                if (index < Components.Count && index >= 0)
                {
                    return Components[index];
                }

                throw new IndexOutOfRangeException(
                    $"Component index ({index}) must be less than {Components.Count} and greater or equal to 0.");
            }
            set
            {
                if (index < Components.Count && index >= 0)
                {
                    Components[index] = value;
                }

                throw new IndexOutOfRangeException(
                    $"Component index ({index}) must be less than {Components.Count} and greater or equal to 0.");
            }
        }

        public Food() : base()
        {
            Components = new List<GiftItemComponent>();
        }

        public Food(string name, string manufacturer, GiftItemComponent initComponent) : base(name, manufacturer,
            initComponent)
        {
            Components = new List<GiftItemComponent>();
        }

        protected Food(string name, string manufacturer) : base(name, manufacturer)
        {
            Components = new List<GiftItemComponent>();
        }
    }
}