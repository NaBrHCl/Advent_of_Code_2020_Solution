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

        static void ChangeRow(char c, ref int lowerRow, ref int upperRow)
        {
            switch (c)
            {
                case 'F': TakeHalf(false, ref lowerRow, ref upperRow); break;
                case 'B': TakeHalf(true, ref lowerRow, ref upperRow); break;
            }
        }

        static void ChangeColumn(char c, ref int lowerColumn, ref int upperColumn)
        {
            switch (c)
            {
                case 'L': TakeHalf(false, ref lowerColumn, ref upperColumn); break;
                case 'R': TakeHalf(true, ref lowerColumn, ref upperColumn); break;
            }
        }

        static int ProcessSeat(string code)
        {
            const int PIVOT = 7;
            int lowerColumn = 0, upperColumn = 7;
            int lowerRow = 0, upperRow = 127;

            // row
            for (int i = 0; i < PIVOT; i++)
                ChangeRow(code[i], ref lowerRow, ref upperRow);

            // column
            for (int i = PIVOT; i < code.Length; i++)
                ChangeColumn(code[i], ref lowerColumn, ref upperColumn);

            return GetSeatId(upperRow, upperColumn);
        }
    }
}