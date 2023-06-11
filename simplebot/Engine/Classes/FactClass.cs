using Newtonsoft.Json;

namespace simplebot.Engine.Classes; 

public class FactClass {
    [JsonProperty("fact")] public string Fact { get; set; } = null!;
}