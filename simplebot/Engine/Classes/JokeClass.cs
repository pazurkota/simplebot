using Newtonsoft.Json;

namespace simplebot.Engine.Classes; 

public class JokeClass {
    [JsonProperty("joke")] public string Joke { get; set; } = null!;
}