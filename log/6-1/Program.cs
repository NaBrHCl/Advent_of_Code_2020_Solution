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

            string[] groups = GetGroupAnswers(FILE_PATH);

            int sum = 0;

            foreach (string group in groups)
            {
                int answerCount = GetAnswerCount(group);
                sum += answerCount;
                WriteLine(answerCount);
            }

            WriteLine($"The sum is {sum}");
        }

        static string[] GetGroupAnswers(string filePath)
        {
            List<string> groups = new List<string>();

            StreamReader sr = new StreamReader(filePath);

            string answer = string.Empty;

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                if (line != string.Empty)
                    answer += line;
                else
                {
                    groups.Add(answer);
                    answer = String.Empty;
                }
            }

            groups.Add(answer);

            sr.Close();

            return groups.ToArray();
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

        static int GetAnswerCount(string answer)
        {
            answer = ShellSort(answer);
            answer = RemoveRedundantLetter(answer);

            return answer.Length;
        }
    }
}