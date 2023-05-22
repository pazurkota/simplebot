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
}