namespace ConsoleApp2
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number  ");
            var userInput = Console.ReadLine();
            var resultedArray = ProcessInput(userInput);
            //do whatever you want with    resultedArray array
        }

        private static int[] ProcessInput(string? userInput)
        {
            string[] numberGroups = [];

            if (!string.IsNullOrEmpty(userInput))
            {
                //if null then return a empty array(developers decision).
                //nothing   is specified about throwing errors on null input so consider it valid case
                numberGroups = userInput!.Split(",");
            }


            List<int> rawNumbersList = [];
            List<string> errors = [];

            foreach (var numberGroup in numberGroups)
            {
                try
                {
                    // suppose that we need to process all input and show all errors so try is used at this level 
                    rawNumbersList.AddRange(GetNumbers(numberGroup));
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }

            }

            DisplayUniqueNumber(rawNumbersList);
            DisplayErrors(errors);
            return [.. rawNumbersList];
        }

        private static void DisplayUniqueNumber(List<int> rawNumbersList)
        {
            Console.WriteLine("numbers extracted from user input");
            if (rawNumbersList.Count > 0)
            {
                var uniquevalues = new SortedSet<int>(rawNumbersList);                 //  if now sorting was required then  =>  rawNumbersList.ToHashSet();  would work 
                Console.Write(string.Join(" ", uniquevalues));
            }
            else
            {
                Console.WriteLine("No valid numbers found in input");
            }

        }

        private static void DisplayErrors(List<string> errors)
        {
            Console.WriteLine();
            if (errors.Count > 0)
            {
                Console.WriteLine("Errors found ");
                errors.ForEach(e => Console.WriteLine(e));
            }
            else
            {
                Console.WriteLine("No errors encuntered during processing");
            }

        }

        private static IEnumerable<int> GetNumbers(string numberGroup)
        {
            if (numberGroup.Contains('-'))
            {
                return GetNumbersFromRange(numberGroup);
            }
            return [int.Parse(numberGroup)]; // will throw exception if not int. if i have time will rewrite it to have a user friend format
        }

        private static IEnumerable<int> GetNumbersFromRange(string numberGroup)
        {
            var rangeParts = numberGroup.Split('-');
            if (rangeParts.Length != 2)
            {
                throw new ArgumentException($"{numberGroup} is not a valid range format: missing parts   "); // typically you would create a custom exception and only use  " missing parts" text , but no time in this example
            }
            var rangeStart = int.Parse(rangeParts[0]);// throw if not valid
            var rangeEnd = int.Parse(rangeParts[1]);// throw if not valid

            if (rangeEnd < rangeStart)
            {
                throw new ArgumentException($"{numberGroup} is not a valid range format : range start  greater then range end  ");
            }

            return Enumerable.Range(rangeStart, rangeEnd - rangeStart + 1);
        }


    }
}
