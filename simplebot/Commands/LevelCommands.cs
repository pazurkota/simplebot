using DSharpPlus;
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
            Xp = XP,
            Level = level
        };
        
        await ctx.DeferAsync();

        LevelEngine engine = new LevelEngine();
        bool userExist = engine.CheckUserExist(username, guildId);

        if (userExist) {
            DUser storedUser = engine.GetUser(username, guildId);

            DiscordEmbed embed = new DiscordEmbedBuilder() {
                Title = "Profile",
                Description = $"**Username:** `{storedUser.Username}`\n**Level:** `{storedUser.Level}`\n**XP:** `{storedUser.Xp}`",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail() {
                    Url = ctx.Member.AvatarUrl
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
                    Description = "*Your profile has been created!*\nType `/profile` again to see your profile",
                    Color = DiscordColor.Green,
                    Timestamp = DateTime.Now
                };
            }
            else {
                embed = new DiscordEmbedBuilder() {
                    Title = "Profile",
                    Description = "*Unknown error occured!*\nYour profile was not created",
                    Color = DiscordColor.Red,
                    Timestamp = DateTime.Now
                };
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
    }

    [SlashCommand("userxp", "Modifies user's XP")]
    public async Task AddXpAsync(InteractionContext ctx, 
        [Option("user", "Get specified user")] DiscordUser user, 
        [Option("xp", "Get XP to give")] double xpToGive) {
        
        await ctx.DeferAsync();
        DiscordEmbed embed;

        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageGuild)) {
            embed = new DiscordEmbedBuilder {
                Title = "Error",
                Description = "You don't have permission to use this command!",
                Color = DiscordColor.Red,
                Timestamp = DateTime.Now
            };
            
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            return;
        }
        
        LevelEngine engine = new LevelEngine();
        bool userExist = engine.CheckUserExist(user.Username, ctx.Guild.Id);

        if (userExist) {
            engine.GiveXp(user.Username, ctx.Guild.Id, xpToGive); // give xp to user
            string desc = $"Successfully {(xpToGive > 0 ? "gave" : "removed")} {xpToGive} XP to `{user.Username}`";
            
            embed = new DiscordEmbedBuilder {
                Title = "Success",
                Description = desc,
                Color = DiscordColor.Green,
                Timestamp = DateTime.Now
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
        else {
            embed = new DiscordEmbedBuilder {
                Title = "Error",
                Description = "User does not exist",
                Color = DiscordColor.Red,
                Timestamp = DateTime.Now
            }; 
                
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
    }
}