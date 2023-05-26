using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TextParser
{
    class WordCalculator
    {
        private static Dictionary<string, int> Parse(String text)
        {
            Dictionary<string,int> dictionary = new Dictionary<string, int>();
            Regex regex = new Regex(@"\b\w+\b");
            MatchCollection matches = regex.Matches(text);
            foreach (Match a in matches)
            {
                Console.WriteLine(a.Value);
                addWordIntoDictionary(a.Value, dictionary);
            }
            return dictionary = dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        private static void addWordIntoDictionary(String str, Dictionary<string, int> dictionary)
        {
            if (!dictionary.ContainsKey(str))
            {
                dictionary.Add(str, 1);
            }
            else
            {
                int currentWordFrequency = dictionary.GetValueOrDefault(str);
                int newWordFrequency = currentWordFrequency + 1;
                dictionary.Remove(str);
                dictionary.Add(str, newWordFrequency);
            }
        }
    }
}
