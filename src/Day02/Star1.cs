using System.Numerics;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace DanielCarey.Day02;

public class Star1(ILogger<Star1> logger) : IStar
{
    public string Name { get => "Day02.Star1"; }

    record Data(BigInteger Num1, BigInteger Num2);

    public ValueTask RunAsync()
    {
        logger.LogInformation($"{Name}.RunAsync");

        var records = File.ReadAllLines("Data1.txt")
            .Select(line => line.Split(' ') // Split the line by spaces
                .Select(int.Parse) // Convert each part to an integer
                .ToList()) // Create a list of integers for each line
            .ToList();

        int safeCount = 0; // Counter for safe rows

        // Now 'records' is a List<List<int>>, similar to the earlier example

        foreach (var record in records)
        {
            bool isSafe = true;
            bool isIncreasing = false;
            bool isDecreasing = false;

            // Loop through each number and check differences
            for (int i = 0; i < record.Count - 1; i++)
            {
                int currentNum = record[i];
                int nextNum = record[i + 1];
                int difference = nextNum - currentNum;

                if (difference >= 1 && difference <= 3)
                {
                    isIncreasing = true;
                }
                else if (difference >= -3 && difference < 0)
                {
                    isDecreasing = true;
                }
                else
                {
                    // If the difference is not within the accepted range, it's unsafe
                    isSafe = false;
                    break;
                }
            }

            // If both increasing and decreasing were found, it's unsafe
            if (isIncreasing && isDecreasing)
            {
                isSafe = false;
            }

            // If the row is safe, increment the count
            if (isSafe)
            {
                safeCount++;
            }
        }

        // Output the total count of safe rows
        Console.WriteLine($"Total safe rows: {safeCount}");

        WriteLine("Done");
        return ValueTask.CompletedTask;
    }
}
