using Newtonsoft.Json;

namespace simplebot; 

public class ConfigHandler {
    public static Config GetConfig() {
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