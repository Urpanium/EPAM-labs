using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using T1.GiftComponents;
using T1.GiftComponents.EatableComponent;
using T1.GiftComponents.ToyComponent;
using T1.Groups;

namespace T1
{

    [XmlInclude(typeof(ToyComponent))]
    [XmlInclude(typeof(Composition))]
    
    [XmlInclude(typeof(EatableComponent))]
    [XmlInclude(typeof(Taste))]
    
    [XmlInclude(typeof(EatableGroup))]
    
    [XmlInclude(typeof(Eatable))]
    
    [XmlInclude(typeof(GiftItem))]
    [XmlInclude(typeof(GiftItemComponent))]
    
    public class Gift
    {
        [XmlArray("Components"), XmlArrayItem(typeof(GiftItem), ElementName = "Item")]
        public List<GiftItem> Components { get; set; }

        public Gift()
        {
            Components = new List<GiftItem>();
        }

        public GiftItem this[int index]
        {
            get { return Components[index]; }
            set { Components[index] = value; }
        }

        public bool IsEmpty()
        {
            return Components.Count == 0;
        }

        public void Add(GiftItem item)
        {
            Components.Add(item);
        }

        public void Remove(GiftItem item)
        {
            Components.Remove(item);
        }

        public void Remove(int index)
        {
            Components.RemoveAt(index);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Gift\nComponents:\n");
            for (int i = 0; i < Components.Count; i++)
            {
                stringBuilder.Append($"{i + 1}) {Components[i]}\n");
            }

            return stringBuilder.ToString();
        }
    }
}