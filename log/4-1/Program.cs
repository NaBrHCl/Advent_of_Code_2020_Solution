using static System.Console;

namespace test
{
    internal class Program
    {
        // https://adventofcode.com/2020/day/4

        static void Main(string[] args)
        {
            const string FILE_PATH = "./input.txt";
            string[] requiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            List<string> passports = GetPassport(FILE_PATH);

            foreach (string passport in passports)
            {
                WriteLine($"Passport\n{passport}\n");
            }

            bool[] validity = new bool[passports.Count];

            for (int i = 0; i < passports.Count; i++)
                validity[i] = ValidatePassport(passports[i], requiredFields);

            int validityCount = 0;

            foreach (bool flag in validity)
            {
                WriteLine(flag);

                if (flag)
                    validityCount++;
            }

            WriteLine(validityCount);
        }

        static List<string> GetPassport(string filePath)
        {
            List<string> passports = new List<string>();

            StreamReader sr = new StreamReader(filePath);

            string passport = String.Empty;

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                if (line != String.Empty)
                    passport += line + " ";
                else
                {
                    passports.Add(passport);
                    passport = String.Empty;
                }
            }

            passports.Add(passport);

            sr.Close();

            return passports;
        }

        static bool ValidatePassport(string passport, string[] requiredFields)
        {
            const char SENTINEL_CHAR = ':';

            bool[] fieldIncluded = new bool[requiredFields.Length];

            for (int i = 0; i < fieldIncluded.Length; i++)
                fieldIncluded[i] = false;

            // validate

            for (int i = 0; i < passport.Length; i++)
            {
                if (passport[i] == SENTINEL_CHAR)
                {
                    string field = string.Empty;
                    int k = i - 1;

                    while (k >= 0 && passport[k] != '\n' && passport[k] != ' ')
                    {
                        field = field.Insert(0, passport[k].ToString());
                        k--;
                    }

                    Write("");

                    for (int j = 0; j < requiredFields.Length; j++)
                    {
                        if (field == requiredFields[j])
                        {
                            fieldIncluded[j] = true;
                            break;
                        }
                    }
                }
            }

            // return validity

            for (int i = 0; i < fieldIncluded.Length; i++)
            {
                if (!fieldIncluded[i])
                    return false;
            }

            return true;
        }
    }
}