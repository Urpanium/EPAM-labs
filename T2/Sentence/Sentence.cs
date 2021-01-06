using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2.Sentence
{
    public class Sentence
    {
        public List<SentenceItem> Items { get; }

        public int SumLength
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

        public HashSet<SentenceItem> GetUniqueWords()
        {
            HashSet<SentenceItem> result = new HashSet<SentenceItem>();
            foreach (var item in Items)
            {
                if (item is Word )
                {
                    result.Add(item);
                }
            }

            return result;
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