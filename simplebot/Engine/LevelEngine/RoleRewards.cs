using Newtonsoft.Json.Linq;

namespace simplebot.Engine.LevelEngine; 

public class RoleRewards {
    private const string Path = "Engine/LevelEngine/rewards.json";
    
    public void AddReward(int level, ulong roleId) {
        try {
            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var rewards = jsonObj["rewards"].ToObject<List<Rewards>>();
            rewards.Add(new Rewards() {
                Level = level,
                RewardRoleId = roleId
            });

            jsonObj["rewards"] = JArray.FromObject(rewards);
            File.WriteAllText(Path, jsonObj.ToString());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool CanGiveReward(int level) {
        try {
            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var rewards = jsonObj["rewards"].ToObject<List<Rewards>>();
            var user = rewards.Find(x => x.Level == level);

            return user != null;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public ulong GetReward(int level) {
        try {
            string json = File.ReadAllText(Path);
            JObject jsonObj = JObject.Parse(json);

            var rewards = jsonObj["rewards"].ToObject<List<Rewards>>();
            var user = rewards.Find(x => x.Level == level);

            return user.RewardRoleId;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return 0;
        }
    }
}