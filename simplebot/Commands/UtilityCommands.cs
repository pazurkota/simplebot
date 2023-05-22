using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace simplebot.Commands; 

public class UtilityCommands {
    
    [Command("ping")] 
    public async Task PingAsync(CommandContext ctx) {
        var embed = new DiscordEmbedBuilder {
            Description = $"Pong! Your current ping is: {ctx.Client.Ping}ms",
            Color = DiscordColor.Green
        };

        await ctx.Channel.SendMessageAsync(embed);
    }
}