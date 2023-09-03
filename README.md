# 🤖 simplebot

***The current, stabile version of this bot is `1.x`, with is stabile, rich and fuctional. Now we are focused on developing the `v2.0`, witch means:***
- *`v1.x` is under mainanence mode. The PR's (on `develop` branch) will be accepted if changes impacts the current functionality.*
- *All new development will be on `v2_develop` branch.*
- *Developers are encouraged to continue building on `v1.x` until we announce `v2.0` is stable.*

**simplebot is a simple, yet powerful discord bot for your server!**

![IMG1](https://cdn.discordapp.com/attachments/973146682499956746/1119970853896069211/readme_img.png)
## Prerequisites
- [.NET 7.0 Or newer](https://dotnet.microsoft.com/en-us/download)
- [API Ninjas Key](https://api-ninjas.com/api)
- [Discord Application (bot)](https://discord.com/developers/applications)

## Installation

1. Create your [Discord Application](https://discord.com/developers/docs/getting-started#step-1-creating-an-app)
2. Clone the repository and  open it in your IDE
3. Install dependencies
4. Create `config.json` file in `Config` directory with the following content:
```json
{
  "token": "YOUR DISCORD BOT TOKEN",
  "api-ninjas_apikey": "YOUR API NINJAS KEY",
  "xp_multiplier": 0.5,
  "level_cap": 20
}
```
5. Run the project.
## Usage

This bot uses only [Slash Commands](https://support.discord.com/hc/en-us/articles/1500000368501-Slash-Commands-FAQ)

To getting started, go to your Discord Server and type `/help`

![IMG2](https://cdn.discordapp.com/attachments/973146682499956746/1119977268777857024/IMG2.png)

![IMG3](https://cdn.discordapp.com/attachments/973146682499956746/1119977269335691354/IMG3.png)

To change the XP multiplier (amount of XP user will have after each message) can be change in `"xp_multiplier"` field in config file, and the requierd XP to new level can be modified in `"level_cap"` field
## Contributing

Contributions are always welcome!

See `CONTRIBUTING.md` for ways to get started.

Please adhere to this project's `CODE_OF_CONDUCT.md`.


## Acknowledgements

#### Frameworks:
- [DSharpPlus](https://dsharpplus.github.io/DSharpPlus/)
- [RestSharp](https://restsharp.dev)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

#### APIs:
- [API Ninjas](https://api-ninjas.com/api)
- [Excuse API](https://excuser-three.vercel.app)
- [Meme API](https://github.com/D3vd/Meme_Api)
- [Memegen API](https://api.memegen.link/docs)


## License

This project is under [MIT](https://github.com/pazurkota/simplebot/blob/master/LICENSE) license

