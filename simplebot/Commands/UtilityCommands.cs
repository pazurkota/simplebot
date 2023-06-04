using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace simplebot.Commands; 

public class UtilityCommands : ApplicationCommandModule {
    
    [SlashCommand("ping", "Get the current client ping")]
    public async Task PingAsync(InteractionContext ctx) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        var embed = new DiscordEmbedBuilder {
            Description = $":ping_pong: Pong! Current client ping is: {ctx.Client.Ping}ms",
            Color = DiscordColor.Green
        };

        await ctx.Channel.SendMessageAsync(embed);
    }

    [SlashCommand("whois", "Get information about a user")]
    public async Task WhoIsAsync(InteractionContext ctx, [Option("user", "Get specified user")] DiscordUser user) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        DiscordMember member = (DiscordMember) user;
        
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

    [SlashCommand("serverinfo", "Get information about the current server")]
    public async Task ServerInfoAsync(InteractionContext ctx) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
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