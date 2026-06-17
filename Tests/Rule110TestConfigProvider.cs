using NUnit.Framework;
using System.Text.Json;

namespace Rule110.Tests;

public class Rule110TestConfigProvider
{
    public static Rule110TestConfig Instance { get; private set; }
    
    static Rule110TestConfigProvider()
    {
        string json = File.ReadAllText(Path.Combine("Tests", "config.json"));
        Instance = JsonSerializer.Deserialize<Rule110TestConfig>(json,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
    }
}

