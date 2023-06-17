using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using simplebot.Engine.LevelEngine;
using simplebot.Configuration;

namespace simplebot.Commands;

public class LevelCommands : ApplicationCommandModule {
    [SlashCommand("profile", "Shows your profile")]
    public async Task ProfileAsync(InteractionContext ctx) {
        string username = ctx.Member.Username;

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
        DUser member = engine.GetUser(user.Username, ctx.Guild.Id);

        if (userExist) {

            engine.GiveXp(user.Username, ctx.Guild.Id, xpToGive); // give xp to user

            embed = new DiscordEmbedBuilder {
                Title = "Success",
                Description = $"Successfully {(xpToGive > 0 ? "gave" : "removed")} **{xpToGive} XP** to `{user.Username}" +
                $"`\nNew XP: `{(member.Xp + xpToGive > 0 ? $"{(member.Xp + xpToGive).ToString()}" : "0")}` {(engine.LeveledUp ? " (**Level up!**)" : "")}",
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

    [SlashCommand("addreward", "Adds a role reward")]
    public async Task AddRewardAsync(InteractionContext ctx, 
        [Option("role", "Specify the reward role")] DiscordRole role, 
        [Option("level", "Specify a level to give reward")] long level) {

        await ctx.DeferAsync();

        RoleRewards roleRewards = new RoleRewards();
        roleRewards.AddReward((int)level, role.Id);
        
        DiscordEmbed embed = new DiscordEmbedBuilder {
            Title = "Success",
            Description = $"Successfully added role reward {role.Mention} for level `{level}`",
            Color = DiscordColor.Green,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
}