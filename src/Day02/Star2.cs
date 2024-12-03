using System.Numerics;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace DanielCarey.Day02;

public class Star2(ILogger<Star1> logger) : IStar
{
    public string Name { get => "Day02.Star2"; }

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

        foreach (var record in records)
        {
            // First, check if the row is safe without any removal
            bool isSafe = CheckIfSafe(record);

            // If not safe, check if removing one number makes it safe
            if (!isSafe)
            {
                isSafe = CheckIfRemovingOneMakesItSafe(record);
            }

            // If the row is safe (either originally or after removing one number), increment the count
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

    // Check if the report is safe (either strictly increasing or strictly decreasing)
    private bool CheckIfSafe(List<int> record)
    {
        bool isIncreasing = false;
        bool isDecreasing = false;

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
                return false; // Unsafe if any difference is outside the allowed range
            }
        }

        // The row is safe if it's either fully increasing or decreasing, but not both
        return !(isIncreasing && isDecreasing);
    }

    // Check if removing one number makes the report safe
    private bool CheckIfRemovingOneMakesItSafe(List<int> record)
    {
        // Try removing each number in the list one by one and check if the result is safe
        for (int i = 0; i < record.Count; i++)
        {
            var modifiedRecord = record.Where((_, index) => index != i).ToList();

            // If removing the number results in a safe record, return true
            if (CheckIfSafe(modifiedRecord))
            {
                return true;
            }
        }

        return false; // If no removal works, the report is unsafe
    }
}
