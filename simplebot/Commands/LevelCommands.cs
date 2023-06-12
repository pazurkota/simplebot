using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using simplebot.Engine.LevelEngine;

namespace simplebot.Commands;

public class LevelCommands : ApplicationCommandModule {
    [SlashCommand("profile", "Shows your profile")]
    public async Task ProfileAsync(InteractionContext ctx) {
        string username = ctx.Member.Username;
        string avatarUrl = ctx.Member.AvatarUrl;

        ulong guildId = ctx.Guild.Id;
        int XP = 0;
        int level = 0;

        DUser user = new DUser() {
            Username = username,
            GuildId = guildId.ToString(),
            AvatarUrl = avatarUrl,
            Xp = XP,
            Level = level
        };
        
        await ctx.DeferAsync();

        LevelEngine engine = new LevelEngine();
        bool userExist = engine.CheckUserExist(username, guildId);

        if (userExist) {
            DiscordEmbed embed = new DiscordEmbedBuilder() {
                Title = "Profile",
                Description = $"**Username:** {username}\n" +
                              $"**Level:** {user.Level}\n" +
                              $"**XP:** {user.Xp}\n" +
                              $"**Avatar:** [Click here]({avatarUrl})",
                Thumbnail = new() {
                    Url = avatarUrl
                },
                Color = DiscordColor.Blurple,
                Timestamp = DateTime.Now
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
        else {
            bool isStored = engine.StoreUserDetails(user);
            DiscordEmbed embed;

            if (isStored) {
                embed = new DiscordEmbedBuilder() {
                    Title = "Profile",
                    Description = "Your profile has been created!\nType `/profile` again to see your profile",
                    Color = DiscordColor.Green,
                    Timestamp = DateTime.Now
                };
            }
            else {
                embed = new DiscordEmbedBuilder() {
                    Title = "Profile",
                    Description = "Unknown error occured!\nYour profile was not created",
                    Color = DiscordColor.Red,
                    Timestamp = DateTime.Now
                };
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
    }
}