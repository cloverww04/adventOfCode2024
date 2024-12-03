using System.Text.RegularExpressions;
using System.Numerics;
using System.IO;
using Microsoft.Extensions.Logging;

namespace DanielCarey.Day03
{
    public class Star1 : IStar
    {
        private readonly ILogger<Star1> logger;

        public string Name { get => "Day03.Star1"; }

        record Data(BigInteger Num1, BigInteger Num2);

        public Star1(ILogger<Star1> logger)
        {
            this.logger = logger;
        }

        public ValueTask RunAsync()
        {
            logger.LogInformation($"{Name}.RunAsync");

            // Read data from file
            var fileContent = File.ReadAllText("Data1.txt");

            // Define the regex pattern for matching multiplication instructions
            var regexPattern = @"mul\((\d+),(\d+)\)|mul\[(\d+),(\d+)\]";

            // Create a list to hold the multiplication results
            BigInteger total = 0;

            // Find matches using the regex
            var matches = Regex.Matches(fileContent, regexPattern);

            foreach (Match match in matches)
            {
                // Depending on the match, we might need to extract different groups
                if (match.Groups[1].Success && match.Groups[2].Success)
                {
                    // Valid mul(x,y) format
                    var num1 = BigInteger.Parse(match.Groups[1].Value);
                    var num2 = BigInteger.Parse(match.Groups[2].Value);
                    total += num1 * num2;
                }
                else if (match.Groups[3].Success && match.Groups[4].Success)
                {
                    // Valid mul[x,y] format
                    var num1 = BigInteger.Parse(match.Groups[3].Value);
                    var num2 = BigInteger.Parse(match.Groups[4].Value);
                    total += num1 * num2;
                }
            }

            // Log the result
            logger.LogInformation($"Total multiplication result: {total}");

            WriteLine("Done");
            return ValueTask.CompletedTask;
        }
    }
}
