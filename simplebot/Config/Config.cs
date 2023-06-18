using Newtonsoft.Json;

namespace simplebot.Configuration;

internal struct Config {
    [JsonProperty("token")] public string Token { get; private set; }
    [JsonProperty("api-ninjas_apikey")] public string ApiNinjasApiKey { get; private set; }
    [JsonProperty("xp_multiplier")] public double LevelMultiplier { get; private set; }
    [JsonProperty("level_cap")] public int LevelCap { get; private set; }
    
    internal static Config LoadConfig() {
        return JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config/config.json"));
    }
}