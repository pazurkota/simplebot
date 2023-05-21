using Newtonsoft.Json;

namespace simplebot; 

internal struct Config {
    [JsonProperty("token")] public string Token { get; private set; }
    [JsonProperty("prefix")] public string Prefix { get; private set; }
}