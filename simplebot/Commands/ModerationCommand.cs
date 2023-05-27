using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace simplebot.Commands; 

public class ModerationCommand : BaseCommandModule {
    
    [Command("purge"), System.ComponentModel.Description("Deletes a specified amount of messages.")] 
    public async Task PurgeAsync(CommandContext ctx, int amount = 1) {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageMessages)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Deleted {amount} messages!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        var messages = await ctx.Channel.GetMessagesAsync(amount);
        await ctx.Channel.DeleteMessagesAsync(messages);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    [Command("kick"), System.ComponentModel.Description("Kicks a specified user.")]
    public async Task KickAsync(CommandContext ctx, DiscordMember member, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.KickMembers)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully kicked {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        await member.RemoveAsync(reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    [Command("ban"), System.ComponentModel.Description("Bans a specified user.")]
    public async Task BanAsync(CommandContext ctx, DiscordMember member, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully banned {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        await member.BanAsync(0, reason);
        await ctx.Channel.SendMessageAsync(embed);
    }
    
    [Command("tempban"), System.ComponentModel.Description("Temporarily bans a specified user.")]
    public async Task TempbanAsync(CommandContext ctx, DiscordMember member, int time, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers)) {
            await ctx.Channel.SendMessageAsync("You don't have permission to use this command!");
            return;
        }

        var embed = new DiscordEmbedBuilder()
            .WithTitle(":white_check_mark: Success!")
            .WithDescription($"Succesfully temporarily banned {member.Mention}!\nReason: {reason}!")
            .WithColor(DiscordColor.Green)
            .WithTimestamp(DateTime.Now);
        
        await member.BanAsync(time, reason);
        await ctx.Channel.SendMessageAsync(embed);
    }

    [Command("unban"), System.ComponentModel.Description("Unbans a specified user.")]
    public async Task UnbanAsync(CommandContext ctx, DiscordUser user, [RemainingText] string reason = "No reason provided.") {
        if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers)) {
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
}