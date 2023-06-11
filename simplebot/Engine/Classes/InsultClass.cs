using Newtonsoft.Json;

namespace simplebot.Engine.Classes;

public class InsultClass {
    [JsonProperty("insult")] public string Insult { get; set; } = null!;
    [JsonProperty("createdby")] public string Author { get; set; } = null!;
}