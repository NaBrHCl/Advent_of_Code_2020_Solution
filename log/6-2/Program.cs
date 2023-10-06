using System.Text;
using static System.Console;

namespace test
{
    internal class Program
    {
        // https://adventofcode.com/2020/day/6

        static void Main(string[] args)
        {
            const string FILE_PATH = "./input.txt";

            StreamReader sr = new StreamReader(FILE_PATH);

            int count = 0;

            while (!sr.EndOfStream)
            {
                string[] answers = GetAnswerFromOneGroup(sr);

                string containedLetters = GetContainedLetters(answers);

                count += CountCommonAnswer(containedLetters, answers);
            }

            WriteLine($"The sum is {count}");
        }

        static int CountCommonAnswer(string letters, string[] answers)
        {
            int count = 0;

            foreach(char letter in letters)
            {
                if (CheckIsCommon(letter, answers))
                    count++;
            }

            return count;
        }

        static string[] GetAnswerFromOneGroup(StreamReader sr)
        {
            List<string> answers = new List<string>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                if (line != String.Empty)
                    answers.Add(line);
                else
                    return answers.ToArray();
            }

            return answers.ToArray();
        }

        static string GetContainedLetters(string[] arr)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (string s in arr)
                sb.Append(s);

            string a = sb.ToString();

            a = ShellSort(a);
            a = RemoveRedundantLetter(a);

            return a;
        }

        static bool CheckIsCommon(char key, string[] answers)
        {
            foreach (string answer in answers)
            {
                bool containsKey = false;

                foreach (char c in answer)
                {
                    if (c == key)
                    {
                        containsKey = true;
                        break;
                    }
                }

                if (!containsKey)
                    return false;
            }

            return true;
        }

        static string ShellSort(string str)
        {
            char[] a = str.ToCharArray();

            for (int gap = a.Length / 2; gap >= 1; gap /= 2)
            {
                for (int j = gap; j < a.Length; j++)
                {
                    for (int i = j - gap; i >= 0; i -= gap)
                    {
                        if (a[i] > a[i + gap])
                        {
                            // swap a[i] with a[i + gap]
                            char c = a[i];
                            a[i] = a[i + gap];
                            a[i + gap] = c;
                        }
                    }
                }
            }

            return new string(a);
        }

        static string RemoveRedundantLetter(string str)
        {
            StringBuilder a = new StringBuilder();

            a.Append(str[0]);

            for (int i = 1; i < str.Length; i++)
            {
                if (a[a.Length - 1] != str[i])
                    a.Append(str[i]);
            }

            return a.ToString();
        }
    }
}