using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2
{
    public class Sentence
    {
        public List<SentenceItem> Items { get; }

        public int Length
        {
            get
            {
                int sum = 0;
                foreach (var item in Items)
                {
                    sum += item.Length;
                }

                return sum;
            }
        }

        public int WordsCount => Items.OfType<Word>().Count();


        public Sentence()
        {
            Items = new List<SentenceItem>();
        }

        public Sentence(IEnumerable<SentenceItem> items)
        {
            Items = items.ToList();
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in Items)
            {
                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}