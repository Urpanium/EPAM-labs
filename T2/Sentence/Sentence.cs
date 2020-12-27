using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2
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

        //TODO: make it return <Word>
        public HashSet<SentenceItem> GetUniqueWords()
        {
            /*var words = from w in Items
                where w is Word
                select w;

            return new HashSet<SentenceItem>(words.ToList());*/
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