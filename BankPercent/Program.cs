using System;

namespace BankPercent
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(
                "Enter starting sum, interest rate in percents, deposit duration in months:");
            var userInput = Console.ReadLine();

            double finalSum = Calculate(userInput);
            
            Console.WriteLine($"Final sum with full interest: {finalSum}");
        }

        public static double Calculate(string userInput)
        {
            var numbers = userInput.Split();

            double initialSum = double.Parse(numbers[0]);
            double yearlyInterestRatePercent = double.Parse(numbers[1]);
            double durationMonths = double.Parse(numbers[2]);

            double monthlyInterestRate = yearlyInterestRatePercent / 100.0 / 12.0;
            double fullInterestRate = Math.Pow(1 + monthlyInterestRate, durationMonths);

            return initialSum * fullInterestRate;
        }
    }
}