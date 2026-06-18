using NUnit.Framework;

namespace Rule110.Tests;

public class Rule110TestConfig
{
    public TestMode Mode { 
        get => Recording == "y" 
            ? TestMode.Recording 
            : TestMode.Testing; 
    }

    public string Recording { get; set; }
}

