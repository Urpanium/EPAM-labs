using System.Collections.Generic;
using System.Text;

namespace T1
{
    public class Gift
    {
        
        public List<GiftItem> Components { get; set; }

        public Gift()
        {
            Components = new List<GiftItem>();
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