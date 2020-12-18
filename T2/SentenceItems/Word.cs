using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2
{
    public class Word: SentenceItem
    {
        public override string Value
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (var c in Chars)
                {
                    builder.Append(c);
                }

                return builder.ToString();
            }
            
            set
            {
                Chars.Clear();
                foreach (var c in value)
                {
                    Char character = new Char(c);
                    Chars.Add(character);
                }
            }
            
        }

        public List<Char> Chars;

        public override int Length => Chars.Count();

        public Word()
        {
            Chars = new List<Char>();
        }
        
        public Word(string s)
        {
            Chars = new List<Char>();
            foreach (var c in s)
            {
                Char character = new Char(c);
                Chars.Add(character);
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}