using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using T1.GiftComponents;

namespace T1.Groups
{
    public class EatableGroup : Eatable
    {
        
        
        
        [XmlArray("Eatables"), XmlArrayItem(typeof(Eatable), ElementName = "Item")]
        public List<Eatable> Eatables { get; set; }

        public int Count
        {
            get { return Eatables.Count; }
        }

        public new float Weight
        {
            get
            {
                float sum = 0.0f;
                for (int i = 0; i < Eatables.Count; i++)
                {
                    sum += Eatables[i].Weight;
                }

                return sum;
            }
        }

        public new Eatable this[int index]
        {
            get
            {
                if (index < Eatables.Count && index >= 0)
                {
                    return Eatables[index];
                }

                throw new IndexOutOfRangeException(
                    $"Sweet index ({index}) must be less than {Count} and greater or equal to 0.");
            }
            set
            {
                if (index < Eatables.Count && index >= 0)
                {
                    Eatables[index] = value;
                }

                throw new IndexOutOfRangeException(
                    $"Sweet index ({index}) must be less than {Count} and greater or equal to 0.");
            }
        }

        public EatableGroup(): base()
        {
            Eatables = new List<Eatable>();
        }
        
        public EatableGroup(string name, string manufacturer, GiftItemComponent initComponent) : base(name,
            manufacturer, initComponent)
        {
            Eatables = new List<Eatable>();
        }

        public EatableGroup(string name, string manufacturer, Eatable eatable, int sweetCount) : base(name,
            manufacturer)
        {
            Eatables = new List<Eatable>();
            for (int i = 0; i < sweetCount; i++)
            {
                Eatables.Add(eatable);
            }
        }

        public void Add(Eatable eatable)
        {
            Eatables.Add(eatable);
        }

        public void Add(Eatable eatable, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Eatables.Add(eatable);
            }
        }

        public void Remove(Eatable eatable)
        {
            Eatables.Remove(eatable);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"================\nEatable Group\n{GetInfoString()}\nEatables:\n");
            List<Eatable> uniqueEatables = GetUniqueEatables();
            for (int i = 0; i < uniqueEatables.Count; i++)
            {
                stringBuilder.Append($"{i + 1}. Count: {GetEatableCount(uniqueEatables[i])}\n{uniqueEatables[i]}\n");
            }

            stringBuilder.Append("================");
            return stringBuilder.ToString();
        }

        public string GetInfoString()
        {
            return $"{Name}, manufactured by {Manufacturer} \nWeight: {Weight}";
        }

        private List<Eatable> GetUniqueEatables()
        {
            HashSet<Eatable> uniqueSweets = new HashSet<Eatable>();

            foreach (Eatable sweet in Eatables)
            {
                uniqueSweets.Add(sweet);
            }

            return uniqueSweets.ToList();
        }

        public int GetEatableCount(Eatable eatable)
        {
            int result = 0;
            foreach (Eatable e in Eatables)
            {
                if (e.Equals(eatable))
                    result++;
            }

            return result;
        }
    }
}