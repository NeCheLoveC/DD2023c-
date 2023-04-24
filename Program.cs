﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DDConsole
{
    class Program
    {
        private static Dictionary<String, long> dictionary = new Dictionary<string, long>();
        
        static void Main(string[] args)
        {
            if (System.IO.File.Exists("C:\\Users\\prots\\source\\repos\\DDConsole\\DDConsole\\text.txt"))
            {
                String text = File.ReadAllText("C:\\Users\\prots\\source\\repos\\DDConsole\\DDConsole\\text.txt");
                Regex regex = new Regex(@"\b\w+\b");
                MatchCollection matches = regex.Matches(text);
                foreach (Match a in matches)
                {
                    Console.WriteLine(a.Value);
                    addWord(a.Value);
                }
                dictionary = dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                FileStream stream = File.Create("result.txt");
                writeDictionaryIntoFile(stream);
            }
            else
            {
                Console.WriteLine("Файл не найден... Для выполнения программы необходим файл в " +
                    "корневой папке проекта - 'text.txt'");

            }
            
        }

        private static void addWord(String str)
        {
            if(!dictionary.ContainsKey(str))
            {
                dictionary.Add(str, 1);
            }
            else
            {
                long currentWordFrequency = dictionary.GetValueOrDefault(str);
                long newWordFrequency = currentWordFrequency + 1;
                dictionary.Remove(str);
                dictionary.Add(str, newWordFrequency);
            }
        }
        
        private static void writeDictionaryIntoFile(FileStream file)
        {
            StreamWriter writer = new StreamWriter(file, Encoding.Default);
            foreach(KeyValuePair<String, long> pair in dictionary)
            {
                writer.WriteLine(pair.Key + " : " + pair.Value);
            }
            writer.Close();
            file.Close();
            
        }
    }
}