using System.Text;
using static System.Console;

namespace test
{
    internal class Program
    {
        // https://adventofcode.com/2020/day/5

        static void Main(string[] args)
        {
            const string FILE_PATH = "./input.txt";

            string[] passes = File.ReadAllLines(FILE_PATH);
            int[] seatIDs = new int[passes.Length];

            seatIDs[0] = ProcessSeat(passes[0]);
            WriteLine(seatIDs[0]);
            int max = seatIDs[0];

            for (int i = 1; i < passes.Length; i++)
            {
                seatIDs[i] = ProcessSeat(passes[i]);
                WriteLine(seatIDs[i]);

                if (seatIDs[i] > max)
                    max = seatIDs[i];
            }

            WriteLine($"Max Seat ID is {max}");
        }

        static void TakeHalf(bool upper, ref int lowerBound, ref int upperBound)
        {
            int pivot = (lowerBound + upperBound) / 2;

            if (upper)
                lowerBound = pivot + 1;
            else
                upperBound = pivot;
        }

        static int GetSeatId(int row, int column)
        {
            return row * 8 + column;
        }

        static int ProcessSeat(string code)
        {
            StringBuilder row = new StringBuilder();
            StringBuilder column = new StringBuilder();

            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case 'F': row.Append("0");  break;
                    case 'B': row.Append("1");  break;
                    case 'L': column.Append("0"); break;
                    case 'R': column.Append("1"); break;
                }
            }

            return Convert.ToInt32(row.ToString(), 2) * 8 + Convert.ToInt32(row.ToString(), 2);
        }
    }
}