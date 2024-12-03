using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace DanielCarey.Day03
{
    public class Star2 : IStar
    {
        private readonly ILogger<Star2> logger;

        public string Name { get => "Day03.Star2"; }

        public Star2(ILogger<Star2> logger)
        {
            this.logger = logger;
        }

        public async ValueTask RunAsync()
        {
            logger.LogInformation($"{Name}.RunAsync");

            // Read data from file asynchronously
            var fileContent = await File.ReadAllTextAsync("Data1.txt");

            // Use a regular expression to match mul instructions, do(), and don't() instructions
            string mulPattern = @"mul\((\d+),\s*(\d+)\)";
            string doPattern = @"do\(\)";
            string dontPattern = @"don't\(\)";

            // Split the string by conditions (do() and don't())
            var instructions = Regex.Split(fileContent, $"({doPattern}|{dontPattern})");

            bool mulEnabled = true;
            BigInteger totalSum = 0;

            foreach (var instruction in instructions)
            {
                // If we find a do() instruction, enable mul instructions
                if (Regex.IsMatch(instruction, doPattern))
                {
                    mulEnabled = true;
                    Console.WriteLine("Multiplication enabled.");
                }
                // If we find a don't() instruction, disable mul instructions
                else if (Regex.IsMatch(instruction, dontPattern))
                {
                    mulEnabled = false;
                    Console.WriteLine("Multiplication disabled.");
                }
                // Process valid mul instructions if multiplication is enabled
                else if (mulEnabled)
                {
                    // Find all mul instructions within the current part
                    var mulMatches = Regex.Matches(instruction, mulPattern);

                    foreach (Match match in mulMatches)
                    {
                        if (match.Groups.Count == 3)
                        {
                            try
                            {
                                var num1 = BigInteger.Parse(match.Groups[1].Value);
                                var num2 = BigInteger.Parse(match.Groups[2].Value);

                                var product = num1 * num2;
                                totalSum += product;

                                Console.WriteLine($"Adding multiplication: {num1} * {num2} = {product}");
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine($"Error parsing numbers: {e.Message}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Total result: {totalSum}");
            logger.LogInformation($"Total multiplication result: {totalSum}");
            Console.WriteLine("Done");

            await ValueTask.CompletedTask;
        }
    }
}
