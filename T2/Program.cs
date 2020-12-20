using System;
using System.IO;
using System.Text.RegularExpressions;

namespace T2
{
    /*
     
    Задача 1. Создать программу обработки текста учебника по программированию с использованием классов: Символ, Слово, Предложение, Знак препинания и др. (состав и иерархию классов продумать самостоятельно).
    Во всех задачах с формированием текста заменять табуляции и последовательности пробелов одним пробелом.

    Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
    Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
    Из текста удалить все слова заданной длины, начинающиеся на согласную букву.
    В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.
    
    */
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = ReadFile("input.txt");
            Text text = TextParser.Parse(input);
            foreach (var word in text.Words)
            {
                Console.WriteLine(word);
            }
            //Console.WriteLine(text);
        }

        static string ReadFile(string name)
        {
            return File.ReadAllText(Environment.CurrentDirectory + "/" + name);
        }
    }
}