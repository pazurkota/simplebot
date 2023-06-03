using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace simplebot.Commands; 

public class ModerationCommands : ApplicationCommandModule {     // @TODO Replace other commands with slash commands
    [SlashCommand("purge", "Deletes a specified amount of messages")]
    public async Task PurgeAsync(InteractionContext ctx, [Option("amount", "Amount of message to delete")] long amount) {
        
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageMessages)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Deleted {amount} messages!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        var messages = await ctx.Channel.GetMessagesAsync(int.Parse(amount.ToString()));
        await ctx.Channel.DeleteMessagesAsync(messages);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    
    [SlashCommand("kick", "Kicks a specified user")]
    public async Task KickAsync(InteractionContext ctx, 
        [Option("user", "Get specified user")] DiscordUser user, 
        [Option("reason", "Get specified reason (not required)")] string reason = "No reason provided.") {
        
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.KickMembers)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }
        
        DiscordMember member = (DiscordMember) user;

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully kicked {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        await member.RemoveAsync(reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    
    [SlashCommand("ban", "Bans a specified user")]
    public async Task BanAsync(InteractionContext ctx, 
        [Option("user", "Get specified user")] DiscordUser user, 
        [Option("reason", "Get specified reason (not required)")] [RemainingText] string reason = "No reason provided.") {
        
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        DiscordMember member = (DiscordMember) user;
        
        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully banned {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        await member.BanAsync(0, reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    
    [SlashCommand("tempban", "Temporarily bans a specified user")]
    public async Task TempbanAsync(InteractionContext ctx, 
        [Option("user", "Get specified user")] DiscordUser user, 
        [Option("ban", "Get a ban lenght (default = 1 day)", true)] long days = 1, 
        [Option("reason", "Get specified reason (not required)", true)] [RemainingText] string reason = "No reason provided.") {
        
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var time = new TimeSpan();
        time = TimeSpan.FromDays(days);

        DiscordMember member = (DiscordMember)user;
        
            var embed = new DiscordEmbedBuilder()
                .WithTitle(":white_check_mark: Success!")
                .WithDescription($"Succesfully temporarily banned {member.Mention}!\nReason: {reason}!")
                .WithColor(DiscordColor.Green)
                .WithTimestamp(DateTime.Now);
        
        await member.BanAsync(0, reason);
        await ctx.Channel.SendMessageAsync(embed);
        await Task.Delay(time);
        await ctx.Guild.UnbanMemberAsync(member, reason);
    }
    
    
    [SlashCommand("unban", "Unbans a specified user")] // @TODO Fix this command
    public async Task UnbanAsync(CommandContext ctx, DiscordUser user, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageGuild)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully unbanned {user.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        await ctx.Guild.UnbanMemberAsync(user, reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
    

    [Command("mute"), System.ComponentModel.Description("Mutes a specified user.")]
    public async Task MuteAsync(CommandContext ctx, DiscordMember member, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageMessages)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully muted {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        var role = ctx.Guild.GetRole(882733436686059028);
        await member.GrantRoleAsync(role, reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    
    [Command("tempmute"), System.ComponentModel.Description("Temporarily mutes a specified user.")]
    public async Task TempmuteAsync(CommandContext ctx, DiscordMember member, int time, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageMessages)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully temporarily muted {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        var role = ctx.Guild.GetRole(882733436686059028);
        await member.GrantRoleAsync(role, reason);
        await ctx.Channel.SendMessageAsync(embed);
        await Task.Delay(time);
        await member.RevokeRoleAsync(role, reason);
    }
    
    
    [Command("unmute"), System.ComponentModel.Description("Unmutes a specified user.")]
    public async Task UnmuteAsync(CommandContext ctx, DiscordMember member, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageGuild)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully unmuted {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        var role = ctx.Guild.GetRole(882733436686059028);
        await member.RevokeRoleAsync(role, reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
}