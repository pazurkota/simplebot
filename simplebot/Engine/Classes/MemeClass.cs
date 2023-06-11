using Newtonsoft.Json;

namespace simplebot.Engine.Classes;

public class MemeClass {
    [JsonProperty("postLink")] public string PostLink { get; set; } = null!;
    [JsonProperty("subreddit")] public string SubReddit {get; set; } = null!;
    [JsonProperty("title")] public string Title { get; set; } = null!;
    [JsonProperty("url")] public string ImageUrl { get; set; } = null!;
}