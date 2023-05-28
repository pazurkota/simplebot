using Newtonsoft.Json;

namespace simplebot.Classes; 

public class ExcuseClass {
    [JsonProperty("excuse")] public string Excuse { get; set; } = null!;
    [JsonProperty("category")] public string Category { get; set; } = null!;
}