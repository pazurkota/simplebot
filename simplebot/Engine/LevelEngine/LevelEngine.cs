﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using simplebot.Configuration;

namespace simplebot.Engine.LevelEngine; 

public class LevelEngine {
    public bool LeveledUp;
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

    public DUser GetUser(string username, ulong guildId) { 
        using StreamReader sr = new StreamReader(Path);
        
        string json = sr.ReadToEnd();
        LevelJsonFile jsonObj = JsonConvert.DeserializeObject<LevelJsonFile>(json);

        foreach (DUser member in jsonObj.Members) {
            if (member.Username == username && member.GuildId == guildId.ToString()) {
                return new DUser() {
                    Username = member.Username,
                    GuildId = member.GuildId,
                    Xp = member.Xp,
                    Level = member.Level
                };
            }
        }

        return null;
    }

    public bool AddXp(string username, ulong guildId) {
        try {
            double levelMultiplier = Config.LoadConfig().LevelMultiplier;
            int levelCap = Config.LoadConfig().LevelCap;
            
            string json = File.ReadAllText(Path);
            var jsonObj = JObject.Parse(json);
            
            var members = jsonObj["members"].ToObject<List<DUser>>();
            
            foreach (DUser member in members) {
                if (member.Username == username && member.GuildId == guildId.ToString()) {
                    member.Xp += levelMultiplier;
                } 
                if (member.Xp >= levelCap ) {
                    LeveledUp = true;
                    member.Level++;
                    member.Xp = 0;
                }
            }
            
            jsonObj["members"] = JArray.FromObject(members);
            File.WriteAllText(Path, JsonConvert.SerializeObject(jsonObj, Formatting.Indented));

            return true;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool GiveXp(string username, ulong guildId, double xp) {
        try {
            string json = File.ReadAllText(Path);
            var jsonObj = JObject.Parse(json);
            
            var members = jsonObj["members"].ToObject<List<DUser>>();
            
            foreach (var member in members.Where(member => member.Username == username && member.GuildId == guildId.ToString())) {
                member.Xp += xp;
                    
                if (member.Xp >= Config.LoadConfig().LevelCap) {
                    LeveledUp = true;
                    member.Level++;
                    member.Xp = 0;
                }
                    
                if (member.Xp < 0) {
                    member.Xp = 0;
                }
            }
            
            jsonObj["members"] = JArray.FromObject(members);
            File.WriteAllText(Path, JsonConvert.SerializeObject(jsonObj, Formatting.Indented));

            return true;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }
}

internal sealed class LevelJsonFile {
    [JsonProperty("members")] public List<DUser> Members { get; set; } = null!;
}