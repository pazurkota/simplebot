using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus;

namespace simplebot.Commands; 

public class MusicCommands : ApplicationCommandModule {
    
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

        var node = lavalinkInstance.ConnectedNodes.Values.First();
        await node.ConnectAsync(user.Channel);

        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
            return;
        }
        
        var query = await node.Rest.GetTracksAsync(musicUrl, LavalinkSearchType.SoundCloud);
        if (query.LoadResultType is LavalinkLoadResultType.NoMatches or LavalinkLoadResultType.LoadFailed) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to find any songs!"));
            return;
        }
        
        var track = query.Tracks.First();
        await connection.PlayAsync(track);

        var embed = new DiscordEmbedBuilder {
            Title = "Now playing:",
            Description = $"{track.Title} by {track.Author}\n" +
                          $"Duration: {track.Length}\n" +
                          $"Requested by: {ctx.Member.Username}\n" +
                          $"Url: [Click here!]({track.Uri})",
            Color = DiscordColor.Purple,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
}