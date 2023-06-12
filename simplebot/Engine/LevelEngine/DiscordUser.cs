namespace simplebot.Engine.LevelEngine;

public class DiscordUser {
    public string Username { get; set; } = null!;
    public string GuildId { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public int Xp { get; set; }
    public int Level { get; set; }
}