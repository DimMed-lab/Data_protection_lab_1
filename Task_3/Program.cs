using System;
using System.IO;
using System.Numerics;

class Program
{
    static void Main()
    {
        string inputFilePath = "input.txt";
        string outputFilePath = "output.txt";

        using (StreamReader reader = new StreamReader(inputFilePath))
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine("Проверка числа для проверки, является ли оно простым:");

            try
            {
                BigInteger number = BigInteger.Parse(reader.ReadLine());
                bool isPrime = IsPrime(number);

                if (isPrime)
                {
                    writer.WriteLine($"{number} является простым числом.");
                }
                else
                {
                    writer.WriteLine($"{number} не является простым числом.");
                }
            }
            catch (Exception e)
            {
                writer.WriteLine(e.ToString());
            }

            writer.WriteLine("\nПроверка двух чисел для проверки, являются ли они взаимнопростыми:");

            try
            {
                BigInteger num1 = BigInteger.Parse(reader.ReadLine());
                BigInteger num2 = BigInteger.Parse(reader.ReadLine());
                bool areRelativelyPrime = AreRelativelyPrime(num1, num2);

                if (areRelativelyPrime)
                {
                    writer.WriteLine($"{num1} и {num2} являются взаимнопростыми.");
                }
                else
                {
                    writer.WriteLine($"{num1} и {num2} не являются взаимнопростыми.");
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.ToString());
            }
        }
    }

    // Метод для определения, является ли число простым
    static bool IsPrime(BigInteger num)
    {
        int modulo = 1;
        if (num < 2) return false;
        else if (num < 4) return true;
        else if (num % 2 == 0)
        {
            modulo++;
            return false;
        }
        else for (BigInteger u = 3; u < num / 2; u += 2)
            {
                if (num % u == 0)
                {
                    return false;
                }
                modulo++;
            }
        return true;
    }

    // Метод для проверки, являются ли числа взаимнопростыми
    static bool AreRelativelyPrime(BigInteger a, BigInteger b)
    {
        BigInteger gcd = BigInteger.GreatestCommonDivisor(a, b);
        return gcd == 1;  //Проверяем равен ли 1 НОД чисел a и b
    }
}