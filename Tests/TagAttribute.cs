namespace Rule110.Tests;

[AttributeUsage(AttributeTargets.Class)]
public class TagAttribute : Attribute
{
    public string Name { get; }

    public TagAttribute(string name)
    {
        Name = name;
    }
}
