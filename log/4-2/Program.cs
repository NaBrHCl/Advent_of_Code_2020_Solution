using System.Data;
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

                    while (k >= 0 && passport[k] != ' ')
                    {
                        field = field.Insert(0, passport[k].ToString());
                        k--;
                    }

                    for (int j = 0; j < requiredFields.Length; j++)
                    {
                        if (field == requiredFields[j])
                        {
                            fieldIncluded[j] = true;

                            field = string.Empty;
                            k = i + 1;

                            while (k < passport.Length && passport[k] != ' ')
                            {
                                field += passport[k];
                                k++;
                            }

                            switch (requiredFields[j])
                            {
                                case "byr":
                                    int birthYear = int.Parse(field);
                                    if (birthYear > 2002 || birthYear < 1920)
                                        return false;
                                    break;

                                case "iyr":
                                    int issueYear = int.Parse(field);
                                    if (issueYear < 2010 || issueYear > 2020)
                                        return false;
                                    break;

                                case "eyr":
                                    int expirationYear = int.Parse(field);
                                    if (expirationYear < 2020 || expirationYear > 2030)
                                        return false;
                                    break;

                                case "hgt":
                                    string unit = field.Substring(field.Length - 2);
                                    if (unit != "cm" && unit != "in")
                                        return false;

                                    int height = int.Parse(field.Substring(0, field.Length - 2));

                                    switch (unit)
                                    {
                                        case "cm":
                                            if (height < 150 || height > 193)
                                                return false;
                                            break;
                                        case "in":
                                            if (height < 59 || height > 76)
                                                return false;
                                            break;
                                    }
                                    break;

                                case "hcl":
                                    if (field[0] != '#')
                                        return false;

                                    for (int l = 1; l < field.Length; l++)
                                    {
                                        if (!char.IsNumber(field[l]))
                                        {
                                            int letterCode = (int)field[l];
                                            if (letterCode < 97 || letterCode > 102)
                                                return false;
                                        }
                                    }
                                    break;

                                case "ecl":
                                    string[] acceptedValues = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

                                    bool isValid = false;

                                    foreach (string acceptedValue in acceptedValues)
                                    {
                                        if (field == acceptedValue)
                                        {
                                            isValid = true;
                                            break;
                                        }
                                    }

                                    if (!isValid)
                                        return false;
                                    break;

                                case "pid":
                                    if (field.Length == 9)
                                    {
                                        foreach (char c in field)
                                            if (!char.IsDigit(c))
                                                return false;
                                    }
                                    else
                                        return false;
                                    break;

                                default:
                                    return false;
                            }

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