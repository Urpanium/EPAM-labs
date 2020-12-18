using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2.Text
{
    public class Text
    {
        public List<Sentence> Sentences { get; }

        
        public Text(IEnumerable<Sentence> sentences)
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