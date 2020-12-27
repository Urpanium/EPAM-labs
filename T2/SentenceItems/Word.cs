using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T2
{
    public class Word : SentenceItem
    { 
        static readonly string[] Consonants =
        {
            "q", "w", "r", "t", "p", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "й",
            "ц", "к", "н", "г", "ш", "щ", "з", "х", "ф", "в", "п", "р", "л", "д", "ж", "ч", "с", "м", "т", "ь", "б"
        };

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

        public bool IsStartingWithConsonant()
        {
            return Length != 0 && Consonants.Contains(Chars[0].Value + "");
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            // ReSharper disable once PossibleNullReferenceException
            return Value.Equals((obj as Word).Value);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}