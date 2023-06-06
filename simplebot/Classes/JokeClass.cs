using Newtonsoft.Json;

namespace simplebot.Classes; 

public class JokeClass {
    [JsonProperty("joke")] public string Joke { get; set; } = null!;
}