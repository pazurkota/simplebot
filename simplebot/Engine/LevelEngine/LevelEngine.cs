using Newtonsoft.Json.Linq;

namespace simplebot.Engine.LevelEngine; 

public class LevelEngine {
    public bool StoreUserDetails(DUser user) {
        try {
            string path = "Engine/LevelEngine/userinfo.json";
            
            if (!File.Exists(path)) {
                File.Create(path).Dispose();
                File.WriteAllText(path, "{\n\"members\": []\n}");
            }

            string json = File.ReadAllText(path);
            JObject jsonObj = JObject.Parse(json);

            var members = jsonObj["members"].ToObject<List<DUser>>();
            members.Add(user);

            jsonObj["members"] = JArray.FromObject(members);
            File.WriteAllText(path, jsonObj.ToString());

            return true; // return true if the user was successfully stored
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false; // return false if the user was not successfully stored
        }
    }
}