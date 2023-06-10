using Newtonsoft.Json;

namespace simplebot;

internal struct Config {
    [JsonProperty("token")] public string Token { get; private set; }
    [JsonProperty("api-ninjas_apikey")] public string ApiNinjasApiKey { get; private set; }
}