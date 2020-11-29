using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using T1.GiftComponents;
using T1.GiftComponents.ToyComponent;

namespace T1
{
    
    public class Toy: GiftItem
    {
        
        [XmlArray("Comps"), XmlArrayItem(typeof(ToyComponent), ElementName = "Component")]
        public  List<ToyComponent> Comps;
        
        public new float Weight
        {
            get
            {
                float sum = 0.0f;
                foreach (ToyComponent component in Comps)
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
                foreach (ToyComponent component in Comps)
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
                foreach (ToyComponent component in Comps)
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
                foreach (ToyComponent component in Comps)
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
                foreach (ToyComponent component in Comps)
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
                foreach (ToyComponent component in Comps)
                {
                    sum += Salinity;
                }

                return sum / ComponentsCount;
            }
        }

        public new ToyComponent this[int index]
        {
            get
            {
                if (index < Comps.Count && index >= 0)
                {
                    return (ToyComponent) Comps[index];
                }

                throw new IndexOutOfRangeException($"Component index ({index}) must be less than {Comps.Count} and greater or equal to 0.");
            }
            set
            {
                if (index < Comps.Count && index >= 0)
                {  
                    Comps[index] = value;
                }
                throw new IndexOutOfRangeException($"Component index ({index}) must be less than {Comps.Count} and greater or equal to 0.");
            }
        }

        public Toy(): base()
        {
            Comps = new List<ToyComponent>();
        }
        
        public Toy(string name, string manufacturer, GiftItemComponent initComponent): base(name, manufacturer, initComponent)
        {
            Comps = new List<ToyComponent>();
        }

        protected Toy(string name, string manufacturer): base(name, manufacturer)
        {
            Comps = new List<ToyComponent>();
        }
    }
}