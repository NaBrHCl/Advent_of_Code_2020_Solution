using System.Text;
using static System.Console;

namespace test
{
    internal class Program
    {
        // https://adventofcode.com/2020/day/8

        static void Main(string[] args)
        {
            const string FILE_PATH = "./input.txt";

            string[] instructions = ReadFromFile(FILE_PATH);

            int accumulator = GetCorrectAccumulator(instructions);

            WriteLine(accumulator);
        }

        static string[] ReadFromFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        static void ParseInstruction(string instruction, out int operation, out int argument)
        {
            string[] info = instruction.Split(' ');

            switch (info[0][0])
            {
                case 'n': operation = 0; break; // nop
                case 'a': operation = 1; break; // acc
                case 'j': operation = 2; break; // jmp
                default: throw new Exception("operation not found");
            }

            argument = int.Parse(info[1]);
        }

        static int GetCorrectAccumulator(string[] instructions)
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                if (TryFlipInstruction(instructions, i))
                {
                    if (TryRunInstructions(instructions, out int accumulator))
                        return accumulator;
                    else
                        TryFlipInstruction(instructions, i);
                }
            }

            throw new Exception("Correct Accumulator can't be obtained");
        }

        static bool TryFlipInstruction(string[] instructions, int i)
        {
            ParseInstruction(instructions[i], out int operation, out int argument);

            if (operation == 1)
                return false;

            instructions[i] = $"{((operation == 0)? "jmp": "nop")} {argument}";

            return true;
        }

        static bool TryRunInstructions(string[] instructions, out int accumulator)
        {
            accumulator = 0;

            List<int> executedLineNums = new List<int>();

            for (int i = 0; i < instructions.Length; i++)
            {
                if (!CheckIfExecuted(i, executedLineNums))
                {
                    int? jump = RunInstruction(instructions[i], ref accumulator);

                    RecordExecutedLine(i, executedLineNums);

                    if (jump != null)
                        i += jump.Value - 1; // i gets incremented by 1 when looping again
                }
                else
                {
                    accumulator = 0;
                    return false;
                }
            }

            return true;
        }

        // passed in list must be sorted (ascending order)
        static void RecordExecutedLine(int executedLineNum, List<int> executedLineNums)
        {
            int k = 0;

            while (k < executedLineNums.Count && executedLineNums[k] <= executedLineNum)
                k++;

            executedLineNums.Insert(k, executedLineNum);
        }

        static bool CheckIfExecuted(int lineNum, List<int> executedLineNums)
        {
            foreach (int executedLineNum in executedLineNums)
            {
                if (executedLineNum == lineNum)
                    return true;
            }

            return false;
        }

        static int? RunInstruction(string instruction, ref int accumulator)
        {
            ParseInstruction(instruction, out int operation, out int argument);

            if (operation == 1)
                accumulator += argument;
            else if (operation == 2)
                return argument;

            return null;
        }
    }
}