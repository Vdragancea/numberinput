namespace ConsoleApp2
{
    using System;
    using System.Collections.Generic;
     internal class Program
    {
        static void Main(string[] args)
        {
            var numberParser = new NumberParser();
            numberParser.GetUserInput();
            numberParser.ProcessInput();
            numberParser.DisplayUniqueNumber();
            numberParser.DisplayErrors(); 
        } 

    }

    class NumberParser {
        public int[] UniqueNumber { get; set; } = [];
        public List<string> ProcessingErrors { get; set; } = [];
        private string userInput = "";
        private List<int> rawNumbersList = [];

        public void  GetUserInput() {
            Console.WriteLine("Enter number  ");
            this.userInput= Console.ReadLine();
        }

        public void ProcessInput() {
            string[] numberGroups = [];

            if (!string.IsNullOrEmpty(userInput))
            {
                //if null then return a empty array(developers decision).
                //nothing   is specified about throwing errors on null input so consider it valid case
                numberGroups = userInput!.Split(",");
            } 

            foreach (var numberGroup in numberGroups)
            {
                try
                {
                    // suppose that we need to process all input and show all errors so try is used at this level 
                    rawNumbersList.AddRange(GetNumbers(numberGroup));
                }
                catch (Exception ex)
                {
                    this.ProcessingErrors.Add(ex.Message);
                }

            }
        }

        private static IEnumerable<int> GetNumbers(string numberGroup) {
            if (numberGroup.Contains('-'))
            {
                return GetNumbersFromRange(numberGroup);
            }
            return [int.Parse(numberGroup)]; // will throw exception if not int. if i have time will rewrite it to have a user friend format
        }

        private static IEnumerable<int> GetNumbersFromRange(string numberGroup) {
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


        public  void DisplayUniqueNumber() {
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

        public   void DisplayErrors() {
            Console.WriteLine();
            if (this.ProcessingErrors.Count > 0)
            {
                Console.WriteLine("Errors found ");
                this.ProcessingErrors.ForEach(e => Console.WriteLine(e));
            }
            else
            {
                Console.WriteLine("No errors encuntered during processing");
            }

        }


    }
}
