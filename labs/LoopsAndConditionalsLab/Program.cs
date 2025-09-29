using System;

public class Program
{
    public static void Main(string[] args)
    {
        int sumFor = CalculateEvenSumWForLoop();
        Console.WriteLine(sumFor);
        if (sumFor >= 2000)
        {
            Console.WriteLine("That’s a big number!");
        }
        int sumWhile = CalculateEvenSumWWhileLoop();
        Console.WriteLine(sumWhile);
        int sumForeach = CalculateEvenSumWForEachLoop();
        string res = sumForeach >= 2000 ? "That’s a big number!" : "That’s a small number.";
        Console.WriteLine(sumForeach + ". " + res);

        Console.WriteLine(GetGradeIf(85));
        Console.WriteLine(GetGradeSwitch(72));

    }

    // task 1: Sum of even numbers from 1 to 100
    public static int CalculateEvenSumWForLoop()
    {
        int sum = 0;

        for (int i = 1; i <= 100; i++)
        {
            if (i % 2 == 0)
            {
                sum += i;
            }
        }

        return sum;
    }

    public static int CalculateEvenSumWWhileLoop()
    {
        int sum = 0;
        int i = 1;
        while (i <= 100)
        {
            if (i % 2 == 0)
            {
                sum += i;
            }
            i++;
        }
        return sum;
    }

    public static int CalculateEvenSumWForEachLoop()
    {
        int sum = 0;
        int[] numbers = new int[100];
        for (int i = 0; i < 100; i++)
        {
            numbers[i] = i + 1;
        }
        foreach (int number in numbers)
        {
            if (number % 2 == 0)
            {
                sum += number;
            }
        }
        return sum;
    }

    // Task 2: Grading with conditonals
    public static char GetGradeIf(int score)
    {
        if (score >= 90)
        {
            return 'A';
        }
        else if (score >= 80)
        {
            return 'B';
        }
        else if (score >= 70)
        {
            return 'C';
        }
        else if (score >= 60)
        {
            return 'D';
        }
        else
        {
            return 'F';
        }
    }

    public static char GetGradeSwitch(int score)
    {
        switch (score / 10)
        {
            case 10:
            case 9:
                return 'A';
            case 8:
                return 'B';
            case 7:
                return 'C';
            case 6:
                return 'D';
            default:
                return 'F';
        }
    }
}

