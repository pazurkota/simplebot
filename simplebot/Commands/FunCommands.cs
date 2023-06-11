using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace simplebot.Commands; 

public class FunCommands : ApplicationCommandModule {
    [SlashCommand("8ball", "Ask the magic 8ball a question")]
    public async Task EightBallAsync(InteractionContext ctx, [Option("input", "Type in everything you want")] string input) {
        await ctx.DeferAsync();
        
        string[] responses = {
            "Yes",
            "No",
            "Maybe",
            "Ask again later",
            "I don't know",
            "I don't think so",
            "I think so",
        };

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = ":8ball: 8ball",
            Description = $"Question: {input}\nAnswer: {responses[new Random().Next(0, responses.Length)]}",
            Color = DiscordColor.Gold,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("excuse", "Get a random excuse")]
    public async Task ExcuseAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        simplebot.Engine.Api.IDataFetcher fetcher = new simplebot.Engine.Api.ExcuseApiFetcher();
        simplebot.Engine.Api.IDataParser parser = new simplebot.Engine.Api.ExcuseApiParser();
        
        var excuse = new simplebot.Engine.Api.ExcuseApiProcessor(fetcher, parser).ProcessData();
        
        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = "Random Excuse",
            Description = $"**Excuse:** `{excuse[0].Excuse}`\n**Category:** `{excuse[0].Category}`",
            Color = DiscordColor.Magenta,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("fact", "Get a random fact")]
    public async Task RandomFactAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        simplebot.Engine.Api.IDataFetcher fetcher = new simplebot.Engine.Api.FactApiFetcher();
        simplebot.Engine.Api.IDataParser parser = new simplebot.Engine.Api.FactApiParser();
        
        var fact = new simplebot.Engine.Api.FactApiProcessor(fetcher, parser).ProcessData();

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = ":question: Did you know:",
            Description = $"`{fact[0].Fact}`",
            Color = DiscordColor.DarkGreen,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("joke", "Get a random joke")]
    public async Task RandomJokeAsync(InteractionContext ctx) {
        await ctx.DeferAsync();
        
        simplebot.Engine.Api.IDataFetcher fetcher = new simplebot.Engine.Api.JokeApiFetcher();
        simplebot.Engine.Api.IDataParser parser = new simplebot.Engine.Api.JokeApiParser();
        
        var joke = new simplebot.Engine.Api.JokeApiProcessor(fetcher, parser).ProcessData();
        
        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = ":rofl: Random joke:",
            Description = $"`{joke[0].Joke}`",
            Color = DiscordColor.Red,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
    
    [SlashCommand("insult", "Insult someone")]
    public async Task InsultAsync(InteractionContext ctx, [Option("user", "Choose a user to insult")] DiscordUser user) {
        await ctx.DeferAsync();
        
        simplebot.Engine.Api.IDataFetcher fetcher = new simplebot.Engine.Api.InsultApiFetcher();
        simplebot.Engine.Api.ISingleDataParser parser = new simplebot.Engine.Api.InsultApiParser();
        
        var insult = new simplebot.Engine.Api.InsultApiProcessor(fetcher, parser).ProcessData();

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = $":face_with_symbols_over_mouth: Hey, {user.Username}:",
            Description = $"`{insult.Insult}`\nAuthor: `{insult.Author}`",
            Color = DiscordColor.Red,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed).WithContent($"{user.Mention}"));
    }
    
    [SlashCommand("meme", "Get a random meme")]
    public async Task RandomMemeAsync(InteractionContext ctx) {
        await ctx.DeferAsync();

        simplebot.Engine.Api.IDataFetcher fetcher = new simplebot.Engine.Api.MemeApiFetcher();
        simplebot.Engine.Api.ISingleDataParser parser = new simplebot.Engine.Api.MemeApiParser();

        var meme = new simplebot.Engine.Api.MemeApiProcessor(fetcher, parser).ProcessData();

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = $"{meme.Title}",
            Description = $"From [r/{meme.SubReddit}]({meme.PostLink})",
            ImageUrl = meme.ImageUrl,
            Color = DiscordColor.Blurple,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    [SlashCommand("generatememe", "Generate a meme")]
    public async Task GenerateMemeAsync(InteractionContext ctx,
    [Option("image", "The image you want to use")] string image,
    [Option("top", "The text you want to put on top of the image")] string top,
    [Option("bottom", "The text you want to put on top of the image", true)] string bottom) {

        await ctx.DeferAsync();
        
        simplebot.Engine.Api.MemeGeneratorApi generator = new simplebot.Engine.Api.MemeGeneratorApi();
        string content = generator.FetchData($"{image}/{top}%2F{bottom}");

        DiscordEmbed embed = new DiscordEmbedBuilder() {
            Title = ":frame_photo: Generated meme:",
            Description = $"[Download]({content})",
            ImageUrl = content,
            Color = DiscordColor.Blurple,
            Timestamp = DateTime.Now
        };

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }
}