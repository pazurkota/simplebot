using Newtonsoft.Json.Linq;

namespace simplebot.Engine.LevelEngine; 

public class Engine {
    public bool StoreUserDetails(DiscordUser user) {
        try {
            string path = "Engine/LevelEngine/userinfo.json";

            string json = File.ReadAllText(path);
            JObject jsonObj = JObject.Parse(json);

            var members = jsonObj["members"].ToObject<List<DiscordUser>>();
            File.WriteAllText(path, jsonObj.ToString());

            return true; // return true if the user was successfully stored
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false; // return false if the user was not successfully stored
        }
    }
}