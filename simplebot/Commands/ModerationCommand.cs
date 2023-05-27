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
}