using System;
using System.Numerics;

class Program
{
    static Random random = new Random();

    static void Main()
    {
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            BigInteger prime = GenerateLargePrime(120); // Здесь 128 - битовая длина числа
            Console.WriteLine("Большое простое число: " + prime);
            writer.WriteLine($"{prime} - сгенерированное простое число.");

            // Генерируем большое взаимнопростое число
            BigInteger coprime = GenerateCoprime(prime, 120); // Здесь 128 - битовая длина числа
            Console.WriteLine("Большое взаимнопростое число: " + coprime);
            writer.WriteLine($"{coprime} - сгенерированное взаимнопростое число.");
        }
    }

    static bool IsPrime(BigInteger num)
    {
        //int modulo = 1;
        if (num < 2) return false;
        else if (num < 4) return true;
        else if (num % 2 == 0)
        {
            //modulo++;
            return false;
        }
        else for (BigInteger u = 3; u < num / 2; u += 2)
            {
                if (num % u == 0)
                {
                    return false;
                }
                //modulo++;
            }
        return true;
    }

    static BigInteger GenerateLargePrime(int bitLength)
    {
        while (true)
        {
            BigInteger candidate = BigIntegerExtensions.RandomInRange(
                BigInteger.Pow(2, bitLength - 1), BigInteger.Pow(2, bitLength));
            if (IsPrime(candidate))
                return candidate;
        }
    }

    static BigInteger GenerateCoprime(BigInteger n, int bitLength)
    {
        while (true)
        {
            BigInteger candidate = BigIntegerExtensions.RandomInRange(
                BigInteger.Pow(2, bitLength - 1), BigInteger.Pow(2, bitLength));
            if (BigInteger.GreatestCommonDivisor(n, candidate) == 1)
                return candidate;
        }
    }
}

public static class BigIntegerExtensions
{
    public static BigInteger RandomInRange(BigInteger min, BigInteger max)
    {
        byte[] data = new byte[max.ToByteArray().Length];
        new Random().NextBytes(data);
        BigInteger generatedValue = new BigInteger(data);

        return (generatedValue % (max - min)) + min;
    }
}