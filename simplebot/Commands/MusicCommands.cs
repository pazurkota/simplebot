using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus;

namespace simplebot.Commands; 

public class MusicCommands : ApplicationCommandModule {
    private static readonly List<LavalinkTrack> Queue = new();
    
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
        
        if (Queue.Count != 0) {
            Queue.Add(track);
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Added {track.Title} to the queue!"));
            return;
        }
        
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

    [SlashCommand("queue", "Current queue")]
    public async Task QueueCommandAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        if (Queue.Count == 0) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There are no songs in the queue!"));
            return;
        }

        string str = "";
        Queue.ForEach(x => str += $"**{Queue.IndexOf(x) + 1}.** {x.Title} by {x.Author}\n");
        
        var embed = new DiscordEmbedBuilder {
            Title = "Queue:",
            Description = str,
            Color = DiscordColor.Purple,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
}