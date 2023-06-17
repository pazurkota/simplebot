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
}