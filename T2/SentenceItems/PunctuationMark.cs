
namespace T2
{
    public class PunctuationMark : SentenceItem
    {
        public override string Value { get; set; }

        public override int Length => Value.Length;

        //private static string[] SentenceDividers = {".", "!", "?", "?!", "!?"};
        
        public PunctuationMark()
        {
            
        }

        public PunctuationMark(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}