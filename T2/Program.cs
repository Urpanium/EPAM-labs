using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace T2
{
    /*
     
    Задача 1. Создать программу обработки текста учебника по программированию с использованием классов: Символ, Слово, Предложение, Знак препинания и др. (состав и иерархию классов продумать самостоятельно).
    Во всех задачах с формированием текста заменять табуляции и последовательности пробелов одним пробелом.

    1. Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
    2. Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
    3. Из текста удалить все слова заданной длины, начинающиеся на согласную букву.
    4. В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.
    
    */
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = ReadFile("input.txt");
            Console.WriteLine("INPUT: " + input);
            Text text = TextParser.Parse(input);
            //1
            var sortedSentences = GetSortedByWordsCountSentences(text);
            
            //2
            var questionSentences = GetQuestionSentences(text);

            foreach (var sentence in questionSentences)
            {
                var words = GetUniqueWordsOfLength(sentence, 6);
            }
            //3
            RemoveWordsThatStartWithConsonant(text, 3);
            
            //4
            ReplaceWordsOfLength(text.Sentences[0], 6, "ururuururu");
            
            Console.WriteLine(text);
            
        }

        static void ReplaceWordsOfLength(Sentence sentence, int wordLengthForReplacement, string replacement)
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
                    if (item is Word && item.Length == desiredLength)
                    {
                        //SentenceItem neighbour;
                        //check neighbours for spaces

                        if (i > 0)
                        {
                            SentenceItem left = sentence.Items[i - 1];
                            if (left.Value.Equals(" "))
                                sentence.Items.RemoveAt(--i);

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
        }

        static IEnumerable<SentenceItem> GetUniqueWordsOfLength(Sentence sentence, int desiredLength)
        {
            //var uniqueWords = sentence.GetUniqueWords();
            var uniqueWords = from w in sentence.GetUniqueWords()
                where w is Word && w.Length == desiredLength
                select w;
            return uniqueWords;
        }

        static IEnumerable<Sentence> GetQuestionSentences(Text text)
        {
            var questionSentences = from s in text.Sentences
                where (s.Items[s.Items.Count - 1]).Value.Equals("?")
                select s;
            return questionSentences;
        }

        static IEnumerable<Sentence> GetSortedByWordsCountSentences(Text text)
        {
            var sortedSentences = from s in text.Sentences
                orderby s.WordsCount
                select s;
            return sortedSentences;
        }

        static string ReadFile(string name)
        {
            //TODO: StreamReader
            return File.ReadAllText(Environment.CurrentDirectory + "/" + name);
        }
    }
}