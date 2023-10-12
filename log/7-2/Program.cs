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

            int count = GetBagCount(KEY_COLOR, bags);

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
            GetChildrenBags(info[1], out string[] containedBagColors, out int[] containedBagCounts);

            return new Bag(color, containedBagColors, containedBagCounts);
        }

        static void GetChildBag(string childBagInfo, out string color, out int count)
        {
            int endIndex = FindFirstStringOccurence(childBagInfo, " bag");
            endIndex--;

            int k = 0;

            StringBuilder sbCount = new StringBuilder();

            while (Char.IsDigit(childBagInfo[k]))
            {
                sbCount.Append(childBagInfo[k]);
                k++;
            }

            count = int.Parse(sbCount.ToString());

            k++;

            StringBuilder sbColor = new StringBuilder();

            for (int i = k; i <= endIndex; i++)
                sbColor.Append(childBagInfo[i]);

            color = sbColor.ToString();
        }

        static void GetChildrenBags(string childrenBagsInfo, out string[] colors, out int[] counts)
        {
            string[] bags = childrenBagsInfo.Split(", ");

            if (bags[0] == "no other bags.")
            {
                colors = new string[0];
                counts = new int[0];
                return;
            }

            colors = new string[bags.Length];
            counts = new int[bags.Length];

            for (int i = 0; i < bags.Length; i++)
                GetChildBag(bags[i], out colors[i], out counts[i]);
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

        static int GetBagCount(string keyColor, List<Bag> bags)
        {
            int count = 0; // the original bag

            CountContainedBags(keyColor, bags, 1, ref count);

            return count;
        }

        static void CountContainedBags(string keyColor, List<Bag> bags, int countOfCurrentBag, ref int count)
        {
            int k = GetBagIndex(keyColor, bags);

            for (int i = 0; i < bags[k].ContainedBagColors.Length; i++)
            {
                count += countOfCurrentBag * bags[k].ContainedBagCounts[i];
                CountContainedBags(bags[k].ContainedBagColors[i], bags, countOfCurrentBag * bags[k].ContainedBagCounts[i], ref count);
            }
        }
    }
}