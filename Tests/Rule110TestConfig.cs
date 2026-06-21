using NUnit.Framework;

namespace Rule110.Tests;

public class Rule110TestConfig
{
    public TestMode Mode { 
        get => Recording
            ? TestMode.Recording 
            : TestMode.Testing; 
    }

    public bool Recording { get; set; }

    public bool WriteToGallery { get; set; }
}

