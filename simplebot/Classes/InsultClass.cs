using Newtonsoft.Json;

namespace simplebot.Classes;

public class InsultClass {
    [JsonProperty("insult")] public string Insult { get; set; } = null!;
    [JsonProperty("createdby")] public string Author { get; set; } = null!;
}