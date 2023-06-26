using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus;

namespace simplebot.Commands; 

public class MusicCommands : ApplicationCommandModule{
    private static readonly List<LavalinkTrack> Queue = new();
    
    [SlashCommand("play", "Plays a specified song")]
    public async Task PlayMusicAsync(InteractionContext ctx, [Option("song", "Get specified song")] string song) {
        await ctx.DeferAsync();
        
        var lavalinkInstance = ctx.Client.GetLavalink();
        var user = ctx.Member.VoiceState.Channel;
        
        if (ctx.Member.VoiceState == null || user == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        if (!lavalinkInstance.ConnectedNodes.Any()) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Lavalink node is not connected!"));
            return;
        }

        if (user.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        var node = lavalinkInstance.ConnectedNodes.Values.First();
        await node.ConnectAsync(user);
        
        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
            return;
        }
        
        var searchQuery = await node.Rest.GetTracksAsync(song);
        if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches || searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"No matches found for : {song}"));
            return;
        }

        var musicTrack = searchQuery.Tracks.First();
        
        if (connection.CurrentState.CurrentTrack != null) {
            Queue.Add(musicTrack);
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Added {musicTrack.Title} to the queue!"));
            return;
        }
        
        await connection.PlayAsync(musicTrack);

        var embed = new DiscordEmbedBuilder() {
            Title = "Now playing:",
            Description = $"{musicTrack.Title} by {musicTrack.Author}\n" +
                          $"Duration: {musicTrack.Length}\n" +
                          $"Requested by: {ctx.Member.Username}\n" +
                          $"Url: [Click here!]({musicTrack.Uri})",
            Color = DiscordColor.Purple,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("queue", "Shows the current queue")]
    public async Task QueueMusicAsync(InteractionContext ctx) {
        await ctx.DeferAsync();

        if (Queue.Count == 0) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There are no songs in the queue!"));
            return;
        }
        
        string queue = "";
        
        foreach (var track in Queue) {
            queue += $"{track.Title} by {track.Author}\n";
        }
        
        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = "Current queue:",
            Description = queue,
            Color = DiscordColor.Purple,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("skip", "Skips the current song")]
    public async Task SkipMusicAsnyc(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        var lavalinkInstance = ctx.Client.GetLavalink();
        var user = ctx.Member.VoiceState.Channel;

        if (ctx.Member.VoiceState == null || user == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }

        if (user.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        var node = lavalinkInstance.ConnectedNodes.Values.First();
        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
            return;
        }

        if (connection.CurrentState.CurrentTrack == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There is no song playing!"));
            return;
        }

        await connection.GetTracksAsync(connection.CurrentState.CurrentTrack.Title);

        if (Queue.Count == 0) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There are no songs in the queue!"));
            return;
        }
        
        var musicTrack = Queue.First();
        Queue.Remove(musicTrack);
        
        await connection.PlayAsync(musicTrack);

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = "Song skipped, now playing:",
            Description = $"{musicTrack.Title} by {musicTrack.Author}\n" +
                          $"Duration: {musicTrack.Length}\n" +
                          $"Requested by: {ctx.Member.Username}\n" +
                          $"Url: [Click here!]({musicTrack.Uri})",
            Color = DiscordColor.Purple,
            Timestamp = DateTime.Now
        };
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("pause", "Pause the current song")]
    public async Task PauseMusicAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        var lavalinkInstance = ctx.Client.GetLavalink();
        var user = ctx.Member.VoiceState.Channel;
        
        if (ctx.Member.VoiceState == null || user == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        if (!lavalinkInstance.ConnectedNodes.Any()) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Lavalink node is not connected!"));
            return;
        }

        if (user.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        var node = lavalinkInstance.ConnectedNodes.Values.First();
        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
            return;
        }

        if (connection.CurrentState.CurrentTrack == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There is no song playing!"));
            return;
        }
        
        await connection.PauseAsync();

        var embed = new DiscordEmbedBuilder() {
            Title = "Stopped playing:",
            Description = $"{connection.CurrentState.CurrentTrack.Title} by {connection.CurrentState.CurrentTrack.Author}\n",
            Color = DiscordColor.Yellow,
            Timestamp = DateTime.Now
        };
            
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("resume", "Resumes the current song")]
    public async Task ResumeMusicAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        var lavalinkInstance = ctx.Client.GetLavalink();
        var user = ctx.Member.VoiceState.Channel;
        
        if (ctx.Member.VoiceState == null || user == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        if (!lavalinkInstance.ConnectedNodes.Any()) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Lavalink node is not connected!"));
            return;
        }

        if (user.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        var node = lavalinkInstance.ConnectedNodes.Values.First();
        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
            return;
        }

        if (connection.CurrentState.CurrentTrack == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There is no song playing!"));
            return;
        }
        
        await connection.ResumeAsync();

        var embed = new DiscordEmbedBuilder() {
            Title = "Resumed playing:",
            Description = $"{connection.CurrentState.CurrentTrack.Title} by {connection.CurrentState.CurrentTrack.Author}\n",
            Color = DiscordColor.Green,
            Timestamp = DateTime.Now
        };
            
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("stop", "Stops the music and disconnecting from the voice channel")]
    public async Task StopMusicAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        var lavalinkInstance = ctx.Client.GetLavalink();
        var user = ctx.Member.VoiceState.Channel;
        
        if (ctx.Member.VoiceState == null || user == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        if (!lavalinkInstance.ConnectedNodes.Any()) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Lavalink node is not connected!"));
            return;
        }

        if (user.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
            return;
        }
        
        var node = lavalinkInstance.ConnectedNodes.Values.First();
        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
            return;
        }

        if (connection.CurrentState.CurrentTrack == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("There is no song playing!"));
            return;
        }
        
        await connection.StopAsync();
        await connection.DisconnectAsync();

        var embed = new DiscordEmbedBuilder() {
            Title = "Stopped playing:",
            Description = $"{connection.CurrentState.CurrentTrack.Title} by {connection.CurrentState.CurrentTrack.Author}\n",
            Color = DiscordColor.Red,
            Timestamp = DateTime.Now
        };
            
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
}