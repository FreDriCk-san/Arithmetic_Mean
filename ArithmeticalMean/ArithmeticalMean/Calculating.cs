using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace ArithmeticalMean
{
    class Calculating
    {

        static void Main(string[] args)
        {
            // Fix UnitTests (multi threading... Async reading and writing file)
            //UnitTests.Test();

            Console.WriteLine("\nEnter the name of your file (in 'Resource' directory): ");
            string fileName = Console.ReadLine();
            string path = UnitTests.CurrDir + "\\Resources\\" + fileName + ".txt";

            try
            {
                Console.WriteLine("\nThe average result is: " + Average(path));
            }
            catch (Exception exception)
            {
                Console.WriteLine("Main Error: " + exception.Message);
            }

            // Tmp
            Console.ReadLine();
        }

        public static double Average(string path)
        {
            try
            {
                var elements = LineParse(path);

                if (null == elements || elements.Count == 0)
                {
                    return 0;
                }

                var sum = 0.0;
                var count = 0;

                foreach(var obj in elements)
                {
                    var value = 0.0;

                    // If current value is double type
                    try
                    {
                        value = Convert.ToDouble(obj, CultureInfo.InvariantCulture);
                        sum += value;
                        count++;
                    }
                    catch { }
                }

                return Math.Abs(sum) / count;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Average Error: " + exception.Message);
            }

            return 0;
        }


        private static ArrayList LineParse(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);

                var numbers = new ArrayList();

                foreach (var line in lines)
                {
                    var builder = new StringBuilder();

                    // Line parsing (if current Char is a number or a symbol)
                    for (int i = 0; i < line.Length; i++)
                    {
                        var currChar = line[i];
                        
                        if (Char.IsDigit(currChar) || currChar.Equals('.') || currChar.Equals('-'))
                        {
                            builder.Append(currChar);

                            // For adding last number
                            if (i == line.Length - 1)
                            {
                                numbers.Add(builder.ToString());
                                builder.Clear();
                            }
                        }
                        else if (builder.Length != 0)
                        {
                            numbers.Add(builder.ToString());
                            builder.Clear();
                        }
                    }

                }

                return numbers;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Line Parse Error: " + exception.Message);
            }

            return null;
        }

    }
}
