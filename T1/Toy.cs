using System;
using System.Collections.Generic;
using T1.GiftComponents;
using T1.GiftComponents.ToyComponent;

namespace T1
{
    public class Toy : GiftItem
    {
        public Toy() : base()
        {
            Components = new List<GiftItemComponent>();
        }

        public Toy(string name, string manufacturer, GiftItemComponent initComponent = null) : base(name, manufacturer,
            initComponent)
        {
            Components = new List<GiftItemComponent>(){initComponent};
        }

        public new List<GiftItemComponent> Components;

        


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
    }
}