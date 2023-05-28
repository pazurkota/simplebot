using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace simplebot.Commands; 

public class UtilityCommands : BaseCommandModule {
    
    [Command("ping")] 
    public async Task PingAsync(CommandContext ctx) {
        var embed = new DiscordEmbedBuilder {
            Description = $":ping_pong: Pong! Your current ping is: {ctx.Client.Ping}ms",
            Color = DiscordColor.Green
        };

        await ctx.Channel.SendMessageAsync(embed);
    }

    [Command("whois")]
    public async Task WhoIsAsync(CommandContext ctx, DiscordMember member) {
        var embed = new DiscordEmbedBuilder() {
            Title = $"Information about {member.DisplayName}",
            Description = $"**Username:** {member.Username}#{member.Discriminator}\n" +
                          $"**ID:** {member.Id}\n" +
                          $"**Creation Date:** {member.CreationTimestamp}\n" +
                          $"**Joined Server:** {member.JoinedAt}\n" +
                          $"**Is Bot:** {member.IsBot}\n" +
                          $"**Nickname:** {member.Nickname ?? "<none>"}\n" +
                          $"**Status:** {member.Presence.Status}\n" +
                          $"**Activity:** {member.Presence.Activity.Name ?? "<none>"}\n" +
                          $"**Roles:** {string.Join(", ", member.Roles.Select(x => x.Mention))}",
            Color = DiscordColor.Azure,
            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail() {
                Url = member.AvatarUrl
            },
            Timestamp = DateTime.Now
        };
        
        await ctx.Channel.SendMessageAsync(embed);
    }

    [Command("serverinfo")]
    public async Task ServerInfoAsync(CommandContext ctx) {
        var embed = new DiscordEmbedBuilder() {
            Title = $"Information about {ctx.Guild.Name}:",
            Description = $"**Name:** {ctx.Guild.Name}\n" +
                          $"**ID:** {ctx.Guild.Id}\n" +
                          $"**Owner:** {ctx.Guild.Owner.Mention}\n" +
                          $"**Creation Date:** {ctx.Guild.CreationTimestamp}\n" +
                          $"**Member Count:** {ctx.Guild.MemberCount}\n" +
                          $"**Role Count:** {ctx.Guild.Roles.Count}\n" +
                          $"**Channel Count:** {ctx.Guild.Channels.Count}\n" +
                          $"**Emoji Count:** {ctx.Guild.Emojis.Count}\n" +
                          $"**Boost Count:** {ctx.Guild.PremiumSubscriptionCount}\n" +
                          $"**Boost Tier:** {ctx.Guild.PremiumTier}\n" +
                          $"**Verification Level:** {ctx.Guild.VerificationLevel}\n" +
                          $"**Explicit Content Filter:** {ctx.Guild.ExplicitContentFilter}\n" +
                          $"**Default Notification Level:** {ctx.Guild.DefaultMessageNotifications}\n" +
                          $"**MFA Level:** {ctx.Guild.MfaLevel}\n" +
                          $"**Is NSFW:** {ctx.Guild.IsNSFW}\n",
            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail() {
                Url = ctx.Guild.IconUrl
            },
            Color = DiscordColor.Azure,
            Timestamp = DateTime.Now
        };

        await ctx.Channel.SendMessageAsync(embed);
    }
}