using DanielCarey;
using System.Numerics;

namespace DanielCarey.DanielCarey.AdventTemplate;

public class Star2(ILogger<Star2> logger) : IStar
{
    public string Name { get => "DanielCarey.AdventTemplate.Star2"; }

    record Data(BigInteger Num1, BigInteger Num2);

    public ValueTask RunAsync()
    {
        logger.LogInformation($"{Name}.RunAsync");

        // Extract Data
        var records = File
            .ReadAllText("Data2.txt")
            .LoadRecords(fields
                => new Data(BigInteger.Parse(fields[0]), BigInteger.Parse(fields[1]))
            );

        // Process Data


        WriteLine("Done");
        return ValueTask.CompletedTask;
    }
}