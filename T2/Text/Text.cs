using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2
{
    public class Text
    {
        public List<Sentence.Sentence> Sentences { get; }

        //TODO: make using LINQ
        public List<Word> Words
        {
            get
            {
                List<Word> result = new List<Word>();
                foreach (var sentence in Sentences)
                {
                    foreach (SentenceItem item in sentence.Items)
                    {
                        if (item is Word)
                            result.Add((Word) item);
                    }
                }

                return result;
            }
        }

        public Text(IEnumerable<Sentence.Sentence> sentences)
        {
            Sentences = sentences.ToList();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var sentence in Sentences)
            {
                builder.Append(sentence);
            }

            return builder.ToString();
        }
    }
}