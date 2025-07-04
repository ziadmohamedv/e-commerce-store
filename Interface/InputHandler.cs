using System;

namespace E_Commerce.Interface
{
    public static class InputHandler
    {
        public static bool GetYesOrNo(string prompt)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(prompt))
                    Console.Write(prompt);
                var input = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty.");
                    continue;
                }

                if (input == "y" || input == "yes")
                    return true;
                if (input == "n" || input == "no")
                    return false;
                Console.WriteLine("Please enter 'y' or 'n'.");
            }
        }

        public static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.");
                return GetUserInput(prompt);
            }
            return input.Trim();
        }

        public static int GetIntegerInput(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty.");
                    continue;
                }

                if (int.TryParse(input, out int result))
                {
                    if (result >= minValue && result <= maxValue)
                        return result;
                    else
                        Console.WriteLine($"Please enter a number between {minValue} and {maxValue}.");
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }
    }
} 