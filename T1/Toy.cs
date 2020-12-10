using System;
using System.Collections.Generic;
using T1.GiftComponents;
using T1.GiftComponents.ToyComponent;

namespace T1
{
    public class Toy : GiftItem, IGiftable
    {
        public Toy() : base()
        {
            Components = new List<GiftItemComponent>();
        }

        public Toy(string name, string manufacturer, GiftItemComponent initComponent) : base(name, manufacturer,
            initComponent)
        {
            Components = new List<GiftItemComponent>();
        }

        protected Toy(string name, string manufacturer) : base(name, manufacturer)
        {
            Components = new List<GiftItemComponent>();
        }

        public new List<GiftItemComponent> Components;

        public new float GetWeight()
        {
            float sum = 0.0f;
            foreach (GiftItemComponent component in Components)
            {
                sum += component.Weight;
            }

            return sum;
        }

        public float GetPrice()
        {
            float sum = 0.0f;
            foreach (GiftItemComponent component in Components)
            {
                sum += component.CalculatedPrice;
            }

            return sum;
        }


        public new ToyComponent this[int index]
        {
            get
            {
                if (index < Components.Count && index >= 0)
                {
                    return (ToyComponent) Components[index];
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
    }
}