using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using simplebot.Engine.LevelEngine;

namespace simplebot.Commands; 

public class LevelCommands : ApplicationCommandModule {
    [SlashCommand("profile", "Shows your profile")]
    public async Task ProfileAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        string username = ctx.Member.Username;
        string guildId = ctx.Guild.Id.ToString();
        string avatarUrl = ctx.Member.AvatarUrl;
        int XP = 0;
        int level = 0;

        DUser user = new DUser() {
            Username = username,
            GuildId = guildId,
            AvatarUrl = avatarUrl,
            Xp = XP,
            Level = level
        };

        LevelEngine engine = new LevelEngine();
        bool process = engine.StoreUserDetails(user);

        if (process) {
            DiscordEmbed embed = new DiscordEmbedBuilder() {
                Title = "Profile",
                Description = $"**Username:** {username}\n" +
                              $"**XP:** {XP}\n" +
                              $"**Level:** {level}",
                Color = DiscordColor.Azure,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail() {
                    Url = avatarUrl
                },
                Timestamp = DateTime.Now
            };
            
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
        else {
            DiscordEmbed failed = new DiscordEmbedBuilder() {
                Title = "Profile",
                Description = "Failed to get your profile",
                Color = DiscordColor.Red,
                Timestamp = DateTime.Now
            };
            
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(failed));
        }
    }
}