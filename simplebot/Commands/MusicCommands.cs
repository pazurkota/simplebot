using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus;

namespace simplebot.Commands; 

public class MusicCommands : ApplicationCommandModule{
    [SlashCommand("play", "Plays a specified song")]
    public async Task PlayMusicAsync(InteractionContext ctx, [Option("song", "Get specified song")] string song) {
        await ctx.DeferAsync();
        
        var lavalinkInstance = ctx.Client.GetLavalink();
        var user = ctx.Member.VoiceState.Channel;
        
        if (ctx.Member.VoiceState == null || user == null)
        {
            await ctx.Channel.SendMessageAsync("Please enter a VC!!!");
        }
        
        if (!lavalinkInstance.ConnectedNodes.Any()) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Lavalink node is not connected!"));
        }

        if (user.Type != ChannelType.Voice) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You must be in a voice channel!"));
        }
        
        var node = lavalinkInstance.ConnectedNodes.Values.First();
        await node.ConnectAsync(user);
        
        var connection = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        if (connection == null) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to connect to voice channel!"));
        }
        
        var searchQuery = await node.Rest.GetTracksAsync(song);
        if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches || searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed) {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"No matches found for : {song}"));
        }

        var musicTrack = searchQuery.Tracks.First();
        
        await connection.PlayAsync(musicTrack);

        var embed = new DiscordEmbedBuilder() {
            Title = "Now playing:",
            Description = $":notes: {musicTrack.Title} by {musicTrack.Author}\n" +
                          $"Duration: {musicTrack.Length}\n" +
                          $"Requested by: {ctx.Member.Username}\n" +
                          $"Url: [Click here!]({musicTrack.Uri})",
            Color = DiscordColor.Purple,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
}