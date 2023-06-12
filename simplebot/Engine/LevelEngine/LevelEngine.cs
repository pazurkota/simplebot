using Newtonsoft.Json.Linq;

namespace simplebot.Engine.LevelEngine; 

public class LevelEngine {
    private const string Path = "Engine/LevelEngine/userinfo.json";
    
    public bool StoreUserDetails(DUser user) {
        try {
            if (!File.Exists(Path)) {
                File.Create(Path).Dispose();
                File.WriteAllText(Path, "{\n\"members\": []\n}");
            }

            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var members = jsonObj["members"].ToObject<List<DUser>>();
            members.Add(user);

            jsonObj["members"] = JArray.FromObject(members);
            File.WriteAllText(Path, jsonObj.ToString());

            return true; // return true if the user was successfully stored
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false; // return false if the user was not successfully stored
        }
    }

    public bool CheckUserExist(string username, ulong guildId) {
        using StreamReader sr = new StreamReader(Path);
        
        string json = sr.ReadToEnd();
        JObject jsonObj = JObject.Parse(json);

        var members = jsonObj["members"].ToObject<List<DUser>>();
        var user = members.Find(x => x.Username == username && x.GuildId == guildId.ToString());

        return user != null;
    }
}