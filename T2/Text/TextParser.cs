using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace T2
{
    public class TextParser
    {
        private static readonly string[] PunctuationMarks = {",", ";", "\"","'", "\n", " ", "—", "-"};
        
        private static readonly string[] SentenceDividers = {".", "!", "?", "?!", "!?", "..."};
        
        public static Text Parse(string text)
        {
            text = Regex.Replace(text, @"( +)|(\t+)", " ");

            List<Sentence> sentences = new List<Sentence>();
            List<SentenceItem> currentItems = new List<SentenceItem>();
            
            
            MatchCollection matchCollection = Regex.Matches(text, @"(\w+\-\w+)|(\w+)|([\W_-[\s]]+)|(\s)");
            foreach (Match match in matchCollection)
            {
                string s = match.Value;
                if (IsWord(s))
                {
                    Word word = new Word(s);
                    currentItems.Add(word);
                }

                if (IsPunctuationMark(s))
                {
                    PunctuationMark punctuationMark = new PunctuationMark(s);
                    currentItems.Add(punctuationMark);
                }

                if (IsSentenceDivider(s))
                {
                    PunctuationMark divider = new PunctuationMark(s);
                    currentItems.Add(divider);
                    
                    Sentence sentence = new Sentence(currentItems);
                    currentItems.Clear();
                    sentences.Add(sentence);
                }
            }

            Text result = new Text(sentences);
            return result;
        }
        
        

        private static bool IsWord(string s)
        {
            return Regex.IsMatch(s, @"(\w+)");
        }

        private static bool IsPunctuationMark(string s)
        {
            //return Regex.IsMatch(s, @"\W");
            return PunctuationMarks.Contains(s);
        }

        private static bool IsSentenceDivider(string s)
        {
            return SentenceDividers.Contains(s);
        }
    }
}