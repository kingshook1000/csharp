using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ifthenelse = new IfThen();
            //ifthenelse.TestIfThen();
            int[][] parties = new int[3][];
            parties[0] = new int[] { 1, 2, 3 };
            parties[1] = new int[] { 10, 20 };

            string[,] values = new string[2, 3];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                    values[i, j] = i.ToString() + j.ToString();
            }
            Console.WriteLine(values[0, 0]);

            Array testArray = Array.CreateInstance(typeof(string), 2, 3, 4);
            Console.WriteLine($"rank:{testArray.Rank}, length:{testArray.Length}");
            for (int i = 0; i < testArray.Rank; i++)
            {
                Console.WriteLine($"Length of dimension:{i} is {testArray.GetLength(i)}, lower bound: {testArray.GetLowerBound(i)}, upper bound: {testArray.GetUpperBound(i)}");
            }
            for (int i = testArray.GetLowerBound(0); i <= testArray.GetUpperBound(0); i++)
                for (int j = testArray.GetLowerBound(1); j <= testArray.GetUpperBound(1); j++)
                    for (int k = testArray.GetLowerBound(2); k <= testArray.GetUpperBound(2); k++)
                    {
                        testArray.SetValue($"{i}-{j}-{k}", i, j, k);
                    }
            foreach (string item in testArray)
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine();
            for (int i = testArray.GetLowerBound(0); i <= testArray.GetUpperBound(0); i++)
            {
                for (int j = testArray.GetLowerBound(1); j <= testArray.GetUpperBound(1); j++)
                {
                    for (int k = testArray.GetLowerBound(2); k <= testArray.GetUpperBound(2); k++)
                    {
                        var value = testArray.GetValue(i, j, k);
                        Console.Write($"{ value} ");
                    }
                    Console.WriteLine();
                }
                
            }
            var arrayEnumerator = testArray.GetEnumerator();
            var currentColumnIndex = 0;
            var col = testArray.GetLength(testArray.Rank - 1);
            while (arrayEnumerator.MoveNext())
            {
                Console.Write($"{arrayEnumerator.Current} ");
                currentColumnIndex++;
                if (currentColumnIndex >= col)
                {
                    currentColumnIndex = default;
                    Console.WriteLine();
                }

            }

        }
    }
}
