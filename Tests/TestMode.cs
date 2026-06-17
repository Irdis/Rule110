using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Rule110.Tests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TestMode
{
    [EnumMember(Value = "recording")]
    Recording,

    [EnumMember(Value = "testing")]
    Testing
}
