
namespace T2
{
    public class PunctuationMark : SentenceItem
    {
        
        public string Value { get; }

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