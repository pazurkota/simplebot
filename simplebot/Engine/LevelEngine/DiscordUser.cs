namespace simplebot.Engine.LevelEngine;

public class DUser {
    public string Username { get; init; } = null!;
    public string GuildId { get; init; } = null!;
    public double Xp { get; set; }
    public int Level { get; set; }
}