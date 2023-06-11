using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using simplebot.Commands;
using simplebot.Configuration;

namespace simplebot; 

public class Bot {
    public DiscordClient Client { get; private set; }
    public CommandsNextExtension Commands { get; private set; }
    public InteractivityExtension Interactivity { get; private set; }

    public async Task RunAsync() {
        var json = Config.LoadConfig();

        var config = new DiscordConfiguration {
            Intents = DiscordIntents.All,
            Token = json.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
        };
        
        Client = new DiscordClient(config);
        
        Client.UseInteractivity(new InteractivityConfiguration {
            Timeout = TimeSpan.FromMinutes(2)
        });
        
        Client.Ready += OnClientReady;
        Client.ComponentInteractionCreated += ButtonPressed;

        var slashCommandConfig = Client.UseSlashCommands();
        
        // slash commands registration
        slashCommandConfig.RegisterCommands<UtilityCommands>();
        slashCommandConfig.RegisterCommands<FunCommands>();
        slashCommandConfig.RegisterCommands<ModerationCommands>();
        
        await Client.ConnectAsync(new DiscordActivity("Powered by SimpleBot", ActivityType.Watching));
        await Task.Delay(-1); // make the bot stay online
    }

    private Task OnClientReady(DiscordClient sender, ReadyEventArgs args) {
        return Task.CompletedTask;
    }

    private Task ButtonPressed(DiscordClient client, ComponentInteractionCreateEventArgs e) {
        DiscordEmbed embed;
        
        switch (e.Interaction.Data.CustomId) {
            case "fun":
                embed = new DiscordEmbedBuilder()
                    .WithTitle("Fun commands help menu")
                    .WithDescription("`/8ball [input]` - Get a random answer to your question\n" +
                                     "`/excuse` - Get a random excuse\n" +
                                     "`/fact` - Get a random fact\n" +
                                     "`/joke` - Get a random joke\n" +
                                     "`/generatememe` Generate a meme\n" +
                                     "`/meme` - Get a random meme\n" +
                                     "`/insult [user]` - Roast a specified user\n")
                    .WithColor(DiscordColor.Azure)
                    .WithTimestamp(DateTime.Now);

                e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, 
                    new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return Task.CompletedTask;
            case "moderation":
                embed = new DiscordEmbedBuilder()
                    .WithTitle("Moderation commands help menu")
                    .WithDescription("`/purge [amount]` - Deletes a specified amount of messages\n" +
                                     "`/kick [user] <reason>` - Kicks a specified user\n" +
                                     "`/ban [user] <reason>` - Bans a specified user\n" +
                                     "`/tempban [user] <time> <reason>` - Temporarily bans a specified user\n" +
                                     "`/tempmute [user] <time> <reason>` - Temporarily bans a specified user\n" +
                                     "`/unban [user]` - Unbans a specified user\n" +
                                     "`/mute [user] <reason>` - Mutes a specified user\n" +
                                     "`/unmute [user]` - Unmutes a specified user\n")
                    .WithColor(DiscordColor.Azure)
                    .WithTimestamp(DateTime.Now);

                    e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, 
                        new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return Task.CompletedTask;
            case "utility":
                embed = new DiscordEmbedBuilder()
                    .WithTitle("Utility commands help menu")
                    .WithDescription("`/ping` - Returns a client's ping\n" +
                                     "`/serverinfo` - Returns information about the server\n" +
                                     "`/whois [user]` - Returns information about a specified user\n" +
                                     "`/help` - Returns this help menu\n" +
                                     "`/repo` - Return the link to the bot's GitHub repository\n" +
                                     "`/support` - Return the link to the bot's support server\n" +
                                     "`/uptime` - Returns the bot's uptime\n")
                    .WithColor(DiscordColor.Azure)
                    .WithTimestamp(DateTime.Now);
                
                e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, 
                    new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return Task.CompletedTask;
            default: // generate error message
                e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
                    new DiscordInteractionResponseBuilder().WithContent("Something went wrong!"));
                return Task.CompletedTask;
        }
    }
}