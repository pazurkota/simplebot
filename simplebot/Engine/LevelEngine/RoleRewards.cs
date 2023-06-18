using Newtonsoft.Json.Linq;

namespace simplebot.Engine.LevelEngine; 

public class RoleRewards {
    private const string Path = "Engine/LevelEngine/rewards.json";
    
    public void AddReward(int level, ulong roleId, ulong guildId) {
        try {
            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var rewards = jsonObj["rewards"].ToObject<List<Rewards>>();
            rewards.Add(new Rewards() {
                Level = level,
                RewardRoleId = roleId,
                GuildId = guildId
            });

            jsonObj["rewards"] = JArray.FromObject(rewards);
            File.WriteAllText(Path, jsonObj.ToString());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool CanGiveReward(int level, ulong guildId) {
        try {
            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var rewards = jsonObj["rewards"].ToObject<List<Rewards>>();
            var user = rewards.Find(x => x.Level == level && x.GuildId == guildId);

            return user != null;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public ulong GetReward(int level, ulong guildId) {
        try {
            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var rewards = jsonObj["rewards"].ToObject<List<Rewards>>();
            var user = rewards.Find(x => x.Level == level && x.GuildId == guildId);

            return user.RewardRoleId;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return 0;
        }
    }
}