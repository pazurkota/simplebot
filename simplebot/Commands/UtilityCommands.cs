﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Diagnostics;

namespace simplebot.Commands; 

public class UtilityCommands : ApplicationCommandModule {
    
    [SlashCommand("ping", "Get the current client ping")]
    public async Task PingAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        DiscordEmbed embed = new DiscordEmbedBuilder {
            Description = $":ping_pong: Pong! Current client ping is: {ctx.Client.Ping}ms",
            Color = DiscordColor.Green
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("whois", "Get information about a user")]
    public async Task WhoIsAsync(InteractionContext ctx, [Option("user", "Get specified user")] DiscordUser user) {
        await ctx.DeferAsync();
        
        DiscordMember member = (DiscordMember) user;
        
        DiscordEmbed embed = new DiscordEmbedBuilder {
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

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("serverinfo", "Get information about the current server")]
    public async Task ServerInfoAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        DiscordEmbed embed = new DiscordEmbedBuilder {
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

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("repo", "Get the link to the bot's GitHub repository")]
    public async Task RepoCommandAsync(InteractionContext ctx) {
        await ctx.DeferAsync();

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = "GitHub Repository:",
            Description = "**Official repository for this bot:**\nhttps://github.com/pazurkota/simplebot",
            Color = DiscordColor.Azure,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("uptime", "Get the bot's uptime")]
    public async Task UptimeCommandAsync(InteractionContext ctx) {
        await ctx.DeferAsync();

        var embed = new DiscordEmbedBuilder() {
            Title = ":clock1: Bot Uptime:",
            Description = $"**Current uptime:** `{DateTime.Now - Process.GetCurrentProcess().StartTime}`",
            Color = DiscordColor.Azure,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("support", "Get the link to the bot's support server")]
    public async Task SupportCommandAsync(InteractionContext ctx) {
        await ctx.DeferAsync();

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = "Support Server:",
            Description = "**Official support server for this bot:**\nhttps://discord.gg/aunWfBPpDY",
            Color = DiscordColor.Azure,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("help", "Get help with the bot")]
    public async Task HelpCommandAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        var funButton = new DiscordButtonComponent(ButtonStyle.Success, "fun", "Fun Commands");
        var moderationButton = new DiscordButtonComponent(ButtonStyle.Success, "moderation", "Moderation Commands");
        var utilityButton = new DiscordButtonComponent(ButtonStyle.Success, "utility", "Utility Commands");
        var musicButton = new DiscordButtonComponent(ButtonStyle.Success, "music", "Music Commands");

        var embed = new DiscordMessageBuilder()
            .WithEmbed(new DiscordEmbedBuilder()
                .WithTitle("Help Menu")
                .WithDescription("Click one of the buttons below to get help with a specific category of commands")
                .WithColor(DiscordColor.Azure)
                .WithTimestamp(DateTime.Now)
            )
            .AddComponents(funButton, moderationButton, utilityButton, musicButton);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Embed).AddComponents(embed.Components));
    }
}