using Newtonsoft.Json;

namespace simplebot;

public struct Config {
    [JsonProperty("token")] public string Token { get; private set; }
    [JsonProperty("api-ninjas_apikey")] public string ApiNinjasApiKey { get; private set; }
}