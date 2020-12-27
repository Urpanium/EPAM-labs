using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace T2
{
    public class TextParser
    {
        private static readonly string[] PunctuationMarks = {",", ":", ";", "'", "\"", "\n", " ", "—", "-"};

        private static readonly string[] SentenceStartPunctuationMarks = {"'", "\""};

        private static readonly string[] SentenceDividers = {".", "!", "?", "?!", "!?", "..."};

        public static Text Parse(string text)
        {
            text = Regex.Replace(text, @"( +\t*)|(\t* +)|( +)|(\t+)|(\r+)", " ");

            List<Sentence> sentences = new List<Sentence>();
            List<SentenceItem> currentItems = new List<SentenceItem>();


            MatchCollection matchCollection =
                Regex.Matches(text, @"(\w+\'\w+)|(\w+\-)+(\w+)|(\w+)|(\.{3})|([\W_-[\s]])|(\s)");

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
                    if (currentItems.Count == 0 && sentences.Count > 0 && !IsSentenceStartPunctuationMark(s))
                    {
                        sentences[sentences.Count - 1].Items.Add(punctuationMark);
                    }
                    else
                    {
                        currentItems.Add(punctuationMark);
                    }
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
            return Regex.IsMatch(s, @"(\w+\-)+(\w+)|(\w+)");
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

        private static bool IsSentenceStartPunctuationMark(string s)
        {
            return SentenceStartPunctuationMarks.Contains(s);
        }
    }
}