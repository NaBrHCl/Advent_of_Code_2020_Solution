using System.Text;
using static System.Console;

namespace test
{
    internal class Program
    {
        // https://adventofcode.com/2020/day/7

        static void Main(string[] args)
        {
            const string FILE_PATH = "./input.txt";
            const string KEY_COLOR = "shiny gold";

            List<Bag> bags = GetBagList(FILE_PATH);

            int count = GetContainCount(KEY_COLOR, bags);

            WriteLine(count);
        }

        static List<Bag> GetBagList(string filePath)
        {
            List<Bag> bags = new List<Bag>();
            
            StreamReader sr = new StreamReader(filePath);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                Bag bag = GetBag(line);

                bags.Add(bag);
            }

            sr.Close();

            return bags;
        }

        static Bag GetBag(string line)
        {
            string[] info = line.Split(" bags contain ");
            string color = info[0];
            string[] containedColors = GetChildrenBags(info[1]);

            return new Bag(color, containedColors);
        }

        static string GetChildBag(string childBagInfo)
        {
            int endIndex = FindFirstStringOccurence(childBagInfo, " bag");
            endIndex--;

            int startIndex = 0;

            while (Char.IsDigit(childBagInfo[startIndex]))
                startIndex++;

            startIndex++;

            StringBuilder sb = new StringBuilder();

            for (int i = startIndex; i <= endIndex; i++)
                sb.Append(childBagInfo[i]);

            return sb.ToString();
        }

        static string[] GetChildrenBags(string childrenBagsInfo)
        {
            string[] bags = childrenBagsInfo.Split(", ");

            if (bags[0] == "no other bags.")
                return new string[0];

            string[] containedColors = new string[bags.Length];

            for (int i = 0; i < bags.Length; i++)
                containedColors[i] = GetChildBag(bags[i]);

            return containedColors;
        }

        static int FindFirstStringOccurence(string str, string key)
        {
            for (int i = 0; i < str.Length; i++) // for each char in str
            {
                if (str[i] == key[0]) // if current char matches first char of key
                {
                    bool ifMatches = true;

                    // start comparing str with key
                    for (int j = 1; j < key.Length; j++)
                    {
                        if ((i + j) >= str.Length || str[i + j] != key[j]) // if i exceeds str length or doesn't match
                        {
                            ifMatches = false;
                            break;
                        }
                    }

                    if (ifMatches)
                        return i;
                }
            }

            return -1;
        }

        static int GetBagIndex(string keyColor, List<Bag> bags)
        {
            for (int i = 0; i < bags.Count; i++)
            {
                if (bags[i].Color == keyColor)
                    return i;
            }

            return -1;
        }

        static int GetContainCount(string keyColor, List<Bag> bags)
        {
            int count = 0;

            for (int i = 0; i < bags.Count; i++)
            {
                if (CheckIfContains(keyColor, i, bags))
                    count++;
            }

            return count;
        }

        static bool CheckIfContains(string keyColor, int i, List<Bag> bags)
        {
            if (bags[i].ContainedColors.Length == 0)
                return false;

            if (bags[i].Contains(keyColor))
                return true;

            foreach (string containedColor in bags[i].ContainedColors)
            {
                if (CheckIfContains(keyColor, GetBagIndex(containedColor, bags), bags))
                    return true;
            }

            return false;
        }
    }
}