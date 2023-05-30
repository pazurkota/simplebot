using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using simplebot.Api;
using simplebot.Classes;

namespace simplebot.Commands; 

public class FunCommands : BaseCommandModule {
    [Command("8ball")]
    public async Task EightBallAsync(CommandContext ctx, string input) {
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

    [Command("excuse")]
    public async Task ExcuseAsync(CommandContext ctx) {
        var excuse = new GetExcuse().ParseData<ExcuseClass>("excuse");
        
        var embed = new DiscordEmbedBuilder() {
            Title = "Random Excuse",
            Description = $"**Excuse:** `{excuse[0].Excuse}`\n**Category:** `{excuse[0].Category}`",
            Color = DiscordColor.Magenta,
            Timestamp = DateTime.Now
        };

        await ctx.Channel.SendMessageAsync(embed);
    }

    [Command("fact")]
    public async Task RandomFactAsync(CommandContext ctx) {
        var fact = new ApiNinjasHandler().ParseData<FactClass>("facts");

        var embed = new DiscordEmbedBuilder() {
            Title = ":question: Did you know:",
            Description = $"`{fact[0].Fact}`",
            Color = DiscordColor.DarkGreen,
            Timestamp = DateTime.Now
        };
        
        await ctx.Channel.SendMessageAsync(embed);
    }

    [Command("joke")]
    public async Task RandomJokeAsync(CommandContext ctx) {
        var joke = new ApiNinjasHandler().ParseData<JokeClass>("jokes");
        
        var embed = new DiscordEmbedBuilder() {
            Title = ":rofl: Random joke:",
            Description = $"`{joke[0].Joke}`",
            Color = DiscordColor.Red,
            Timestamp = DateTime.Now
        };
        
        await ctx.Channel.SendMessageAsync(embed);
    }
}