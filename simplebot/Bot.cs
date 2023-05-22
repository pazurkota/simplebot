using System.Text;
using Newtonsoft.Json;
using DSharpPlus;
using DSharpPlus.CommandsNext;
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
        string json;

        await using (var fs = File.OpenRead("config.json")) {
            using (var sr = new StreamReader(fs, new UTF8Encoding(false))) {
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }
        }
        
        var configJson = JsonConvert.DeserializeObject<Config>(json);

        var config = new DiscordConfiguration {
            Token = configJson.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
        };
        
        Client = new DiscordClient(config);
        Client.UseInteractivity(new InteractivityConfiguration {
            Timeout = TimeSpan.FromMinutes(2)
        });

        var commandsConfig = new CommandsNextConfiguration() {
            StringPrefixes = new [] {configJson.Prefix},
            EnableMentionPrefix = true,
            EnableDms = true,
            EnableDefaultHelp = false
        };

        Commands = Client.UseCommandsNext(commandsConfig);
        
        // commands registration
        Commands.RegisterCommands<UtilityCommands>();
        
        await Client.ConnectAsync();
        await Task.Delay(-1); // make the bot stay online
    }
    
    private Task OnClientReady(ReadyEventArgs e) {
        return Task.CompletedTask;
    }
}