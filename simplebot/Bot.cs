using System.Text;
using Newtonsoft.Json;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using simplebot.Commands;

namespace simplebot; 

public class Bot {
    public DiscordClient Client { get; private set; }
    public CommandsNextExtension Commands { get; private set; }
    public InteractivityExtension Interactivity { get; private set; }

    public async Task RunAsync() {
        var json = ConfigHandler.GetConfig();

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

        var commandsConfig = new CommandsNextConfiguration() {
            StringPrefixes = new [] { json.Prefix },
            EnableMentionPrefix = true,
            EnableDms = true,
            EnableDefaultHelp = true
        };

        Commands = Client.UseCommandsNext(commandsConfig);
        
        // commands registration
        Commands.RegisterCommands<UtilityCommands>();
        Commands.RegisterCommands<ModerationCommand>();
        Commands.RegisterCommands<FunCommands>();
        
        await Client.ConnectAsync(new DiscordActivity("Powered by SimpleBot", ActivityType.Watching));
        await Task.Delay(-1); // make the bot stay online
    }
    
    private Task OnClientReady(ReadyEventArgs e) {
        return Task.CompletedTask;
    }
}