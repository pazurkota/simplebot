using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus;

namespace simplebot.Commands; 

public class MusicCommands : ApplicationCommandModule{
    [SlashCommand("play", "Plays a specified music")]
    public async Task PlayMusicAsync(InteractionContext ctx, [Option("music", "URL of this song")] string musicUrl) {
        await ctx.DeferAsync();
        
        var user = ctx.Member.VoiceState;
        var lavalinkInstance = ctx.Client.GetLavalink();
        
        if (user == null! || user.Channel.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You are not in a voice channel!"));
            return;
        }
        
        if (!lavalinkInstance.ConnectedNodes.Any()) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Lavalink is not connected!"));
            return;
        }
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Command response! (music: {musicUrl})"));
    }
}