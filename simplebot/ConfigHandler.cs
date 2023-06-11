using Newtonsoft.Json;

namespace simplebot; 

internal static class ConfigHandler {
    internal static Config GetConfig() {
        try {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            return config;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}