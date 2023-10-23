using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection.PortableExecutable;

public class MyBigInteger
{
    private List<int> digits;

    public MyBigInteger()
    {
        digits = new List<int>();
    }

    public MyBigInteger(string value)
    {
        digits = new List<int>();
        Console.WriteLine("New digit:\n");
        for (int i = 0; i < value.Length; i++)
        {
            if (char.IsDigit(value[i]))
            {
                digits.Add(value[i] - '0');
                Console.WriteLine($"{value[i]}\n");
            }
        }
    }

    private MyBigInteger Add(MyBigInteger other)
    {
        int carry = 0;
        int maxLength = Math.Max(digits.Count, other.digits.Count);

        int digit1_count = digits.Count;
        int digit2_count = other.digits.Count;
        int difference = Math.Abs(digit2_count - digit1_count);
        //Console.WriteLine($"difference: {difference}");
        if (digit2_count > digit1_count) 
        {
            while(difference > 0) 
            {
                digits.Insert(0,0);
                difference--;
            }
        }
        if (digit2_count < digit1_count)
        {
            while(difference > 0) 
            { 
                other.digits.Insert(0, 0);
                difference--;
            }
        }
        //Console.WriteLine($"first: {digits[2]}, second {other.digits[2]}");


        for (int i = maxLength - 1; i >= 0; i--)
        {
        
            int sum = digits[i] + carry + other.digits[i];
            digits[i] = sum % 10;
            carry = sum / 10;
            //Console.WriteLine($"carry: {carry}\n");

        }
        //Если дошли до конца и у нас остался остаток
        if ((carry != 0))
        {
            //digits.Add(carry);
            digits.Insert(0,carry);
        }
        return this;
    }


    private void Multiply(MyBigInteger multiplier)
    {
        MyBigInteger copy = new MyBigInteger(multiplier.ToString()); // Создаем копию множимого
        MyBigInteger copy_this = new MyBigInteger(this.ToString());             // Создаем MyBigInteger = 0
        MyBigInteger step = new MyBigInteger("1");             // Создаем MyBigInteger = 1
        multiplier = new MyBigInteger("1");


        while (copy.ToString() != multiplier.ToString())
        {
            Console.WriteLine(this.ToString());
            Console.WriteLine(multiplier.ToString());
            this.Add(copy_this);
            multiplier = multiplier + step;
        }

    }

    private void Degree(MyBigInteger degree)
    {
        MyBigInteger copy = new MyBigInteger(degree.ToString()); // Создаем копию степени
        MyBigInteger copy_this = new MyBigInteger(this.ToString());
        MyBigInteger step = new MyBigInteger("1");             // Создаем MyBigInteger = 1
        degree = new MyBigInteger("1");


        while (copy.ToString() != degree.ToString())
        {
            Console.WriteLine(this.ToString());
            Console.WriteLine(degree.ToString());
            this.Add(this);
            //copy_this = this;
            degree = degree + step;
        }

    }



    public override string ToString()
    {
        return new string(digits.ConvertAll(d => (char)(d + '0')).ToArray());
    }


    public static MyBigInteger operator +(MyBigInteger left, MyBigInteger right)
    {
        left.Add(right); // Вызываем метод Add, который реализует сложение
        return left;
    }
    public static MyBigInteger operator *(MyBigInteger left, MyBigInteger right)
    {
        left.Multiply(right); // Вызываем метод Add, который реализует сложение
        return left;
    }
    public static MyBigInteger operator ^(MyBigInteger left, MyBigInteger right)
    {
        left.Degree(right); // Вызываем метод Add, который реализует сложение
        return left;
    }

}



class Program
{
    static void Main()
    {


        string input_filePath = "input.txt";
        string output_filePath = "output.txt";

        try
        {
            // Считать все строки из файла в массив строк
            string[] lines = File.ReadAllLines(input_filePath);

            // Проверить, что в файле есть как минимум две строки
            if (lines.Length >= 6)
            {

                MyBigInteger num1 = new MyBigInteger(lines[0]);
                MyBigInteger num2 = new MyBigInteger(lines[1]);
                MyBigInteger num3 = new MyBigInteger(lines[2]);
                MyBigInteger num4 = new MyBigInteger(lines[3]);
                MyBigInteger num5 = new MyBigInteger(lines[4]);
                MyBigInteger num6 = new MyBigInteger(lines[5]);


                try
                {
                    StreamWriter sw = new StreamWriter("C:\\Users\\dmedv\\source\\repos\\Tests\\output.txt");
                    //MyBigInteger summ = num1 + num2;
                    sw.WriteLine($"{num1} + {num2} = {num1 + num2}\n");
                    sw.WriteLine($"{num3} * {num4} = {num3 * num4}\n");
                    sw.WriteLine($"{num5} ^ {num6} = {num5 ^ num6}\n");
                    sw.Close();
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Произошла ошибка при записи в файл {output_filePath}: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Файл содержит менее двух строк.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден");
        }
        catch (IOException e)
        {
            Console.WriteLine("Произошла ошибка при чтении файла: " + e.Message);
        }
    }
}