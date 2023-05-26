using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TextParser;
using System.Text.RegularExpressions;

namespace DDConsole
{
    class Program
    {
        //private static Dictionary<String, long> dictionary = new Dictionary<string, long>();

        private static String PATH = Directory.GetCurrentDirectory();
        private static String FILE_NAME = "text.txt";

        static void Main(string[] args)
        {
            String fullPathName = PATH + "\\" + FILE_NAME;
            if (System.IO.File.Exists(fullPathName))
            {
                String text = File.ReadAllText(fullPathName);
                Type type = typeof(WordCalculator); 
                Dictionary<String, int> dictionary = (Dictionary<String, int>)type.GetMethod("Parse", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, parameters: new Object[] {(object)text}); ;
                writeDictionaryIntoFile(dictionary);
            }
            else
            {
                Console.WriteLine("Файл не найден... Для выполнения программы необходим файл в " +
                    "корневой папке проекта (" + PATH + ")" + " - text.txt");
            }
        }

        private static void writeDictionaryIntoFile(Dictionary<string,int> dictionary)
        {
            FileStream file = File.Create("result.txt");
            StreamWriter writer = new StreamWriter(file, Encoding.Default);
            foreach(KeyValuePair<String, int> pair in dictionary)
            {
                writer.WriteLine(pair.Key + " : " + pair.Value);
            }
            writer.Close();
            file.Close();
        }
    }
}
