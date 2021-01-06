using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace T2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string inputPath = ConfigurationSettings.AppSettings["FileInputPath"];
            string outputPath = ConfigurationSettings.AppSettings["FileOutputPath"];
            
            try
            {
                string input = ReadFileWithStream(inputPath);
                Text text = TextParser.Parse(input);
                //1
                /*var sortedSentences = GetSortedByWordsCountSentences(text);
                foreach (var sentence in sortedSentences)
                {
                    Console.WriteLine(sentence);
                }*/

                //2
                /*var questionSentences = GetQuestionSentences(text);

                foreach (var sentence in questionSentences)
                {
                    var words = GetUniqueWordsOfLength(sentence, 15);
                    foreach (var word in words)
                    {
                        Console.WriteLine(word);
                    }
                }*/

                //3
                //RemoveWordsThatStartWithConsonant(text, 3);
                

                //4
                //ReplaceWordsOfLength(text.Sentences[0], 6, "ururuururu");
                Console.WriteLine(text);
                //Console.WriteLine(text);*/
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh, something went wrong... Source code here: https://github.com/Urpanium/EPAM-labs, go fix it by yourself. " + e);
            }
            
        }

        static void ReplaceWordsOfLength(Sentence.Sentence sentence, int wordLengthForReplacement, string replacement)
        {
            for (int i = 0; i < sentence.Items.Count; i++)
            {
                SentenceItem item = sentence.Items[i];
                if (item is Word && item.Length == wordLengthForReplacement)
                {
                    sentence.Items[i].Value = replacement;
                }
            }
        }


        static void RemoveWordsThatStartWithConsonant(Text text, int desiredLength)
        {
            foreach (var sentence in text.Sentences)
            {
                for (int i = 0; i < sentence.Items.Count; i++)
                {
                    SentenceItem item = sentence.Items[i];
                    if (item is Word && item.Length == desiredLength && (item as Word).IsStartingWithConsonant())
                    {
                        //check neighbours for spaces
                        if (i > 0)
                        {
                            SentenceItem left = sentence.Items[i - 1];
                            if (left.Value.Equals(" "))
                                sentence.Items.RemoveAt(--i);
                        }
                        else if (i < sentence.Items.Count)
                        {
                            SentenceItem right = sentence.Items[i + 1];
                            if (right.Value.Equals(" "))
                                sentence.Items.RemoveAt(i + 1);
                        }
                        
                        sentence.Items.RemoveAt(i);
                    }
                }
            }
        }

        static IEnumerable<SentenceItem> GetUniqueWordsOfLength(Sentence.Sentence sentence, int desiredLength)
        {
            //var uniqueWords = sentence.GetUniqueWords();
            var uniqueWords = from w in sentence.GetUniqueWords()
                where w is Word && w.Length == desiredLength
                select w;
            return uniqueWords;
        }

        static IEnumerable<Sentence.Sentence> GetQuestionSentences(Text text)
        {
            var questionSentences = from s in text.Sentences
                where s.Items[s.Items.Count - 1].Value.Equals("?")
                select s;
            return questionSentences;
        }

        static IEnumerable<Sentence.Sentence> GetSortedByWordsCountSentences(Text text)
        {
            var sortedSentences = from s in text.Sentences
                orderby s.WordsCount
                select s;
            return sortedSentences;
        }


        static string ReadFileWithStream(string path)
        {
            StringBuilder builder = new StringBuilder();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    builder.Append(line);
                }
                streamReader.Close();
            }

            return builder.ToString();
        }
        

        static string ReadFile(string name)
        {
            return File.ReadAllText(Environment.CurrentDirectory + "/" + name);
        }
    }
}