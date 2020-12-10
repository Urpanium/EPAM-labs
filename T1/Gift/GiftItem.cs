using System;
using System.Collections.Generic;
using System.Text;
using T1.GiftComponents;

namespace T1
{
    
    
    public abstract class GiftItem: GiftStuffBase
    {
        /*public string Name{ get; set; }
        public string Manufacturer{ get; set; }
*/
        
        public new List<GiftItemComponent> Components;

        /*public float GetWeight()
        {
                float sum = 0.0f;
                foreach (GiftItemComponent component in Components)
                {
                    sum += component.Weight;
                }

                return sum;
            
        }*/

        /*public float GetPrice()
        {
            
            
                float sum = 0;
                foreach (GiftItemComponent component in Components)
                {
                    sum += component.GetPrice();
                }

                return sum;
            
        }*/
        public int ComponentsCount => Components.Count;

        public GiftItemComponent this[int index]
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

        public GiftItem()
        {
            
        }
        
        public GiftItem(string name, string manufacturer, GiftItemComponent initComponent)
        {
            Name = name;
            Manufacturer = manufacturer;
            Components = new List<GiftItemComponent> {initComponent};
        }

        protected GiftItem(string name, string manufacturer)
        {
            Name = name;
            Manufacturer = manufacturer;
            Components = new List<GiftItemComponent>();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Gift Item\n");
            stringBuilder.Append($"Name: {Name}, manufactured by {Manufacturer}, Weight: {GetWeight()}\n");
            stringBuilder.Append($"Price: {GetPrice()}\n");
            stringBuilder.Append($"Components ({Components.Count}):\n");
            for (int i = 0; i < Components.Count; i++)
            {
                stringBuilder.Append($"{i + 1}* ");
                stringBuilder.Append(Components[i]);
            }

            return stringBuilder.ToString();
        }
    }
}