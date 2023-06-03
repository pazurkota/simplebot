using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using simplebot.Api;

namespace simplebot.Commands; 

public class FunCommands : ApplicationCommandModule {
    [SlashCommand("8ball", "Ask the magic 8ball a question")]
    public async Task EightBallAsync(InteractionContext ctx, [Option("input", "Type in everything you want")] string input) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        var responses = new[] {
            "Yes",
            "No",
            "Maybe",
            "Ask again later",
            "I don't know",
            "I don't think so",
            "I think so",
        };

        var embed = new DiscordEmbedBuilder() {
            Title = ":8ball: 8ball",
            Description = $"Question: {input}\nAnswer: {responses[new Random().Next(0, responses.Length)]}",
            Color = DiscordColor.Gold,
            Timestamp = DateTime.Now
        };

        await ctx.Channel.SendMessageAsync(embed);
    }

    [SlashCommand("excuse", "Get a random excuse")]
    public async Task ExcuseAsync(InteractionContext ctx) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        IDataFetcher fetcher = new ExcuseApiFetcher();
        IDataParser parser = new ExcuseApiParser();
        
        var excuse = new ExcuseApiProcessor(fetcher, parser).ProcessData();
        
        var embed = new DiscordEmbedBuilder() {
            Title = "Random Excuse",
            Description = $"**Excuse:** `{excuse[0].Excuse}`\n**Category:** `{excuse[0].Category}`",
            Color = DiscordColor.Magenta,
            Timestamp = DateTime.Now
        };

        await ctx.Channel.SendMessageAsync(embed);
    }

    [SlashCommand("fact", "Get a random fact")]
    public async Task RandomFactAsync(InteractionContext ctx) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        IDataFetcher fetcher = new FactApiFetcher();
        IDataParser parser = new FactApiParser();
        
        var fact = new FactApiProcessor(fetcher, parser).ProcessData();

        var embed = new DiscordEmbedBuilder() {
            Title = ":question: Did you know:",
            Description = $"`{fact[0].Fact}`",
            Color = DiscordColor.DarkGreen,
            Timestamp = DateTime.Now
        };
        
        await ctx.Channel.SendMessageAsync(embed);
    }

    [SlashCommand("joke", "Get a random joke")]
    public async Task RandomJokeAsync(InteractionContext ctx) {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
            new DiscordInteractionResponseBuilder().WithContent("Thinking..."));
        
        IDataFetcher fetcher = new JokeApiFetcher();
        IDataParser parser = new JokeApiParser();
        
        var joke = new JokeApiProcessor(fetcher, parser).ProcessData();
        
        var embed = new DiscordEmbedBuilder() {
            Title = ":rofl: Random joke:",
            Description = $"`{joke[0].Joke}`",
            Color = DiscordColor.Red,
            Timestamp = DateTime.Now
        };
        
        await ctx.Channel.SendMessageAsync(embed);
    }
}