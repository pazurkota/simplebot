using Newtonsoft.Json;

namespace simplebot.Classes; 

public class FactClass {
    [JsonProperty("fact")] public string Fact { get; set; } = null!;
}