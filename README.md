
# 🤖 simplebot

A Simple Discord bot to entertain your server

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


## Acknowledgements

#### Frameworks:
- [DSharpPlus](tps://dsharpplus.github.io/DSharpPlus/)
- [RestSharp](https://restsharp.dev)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

#### APIs:
- [API Ninjas](https://api-ninjas.com/api)
- [Excuse API](https://excuser-three.vercel.app)
- [Meme API](https://github.com/D3vd/Meme_Api)
- [Memegen API](https://api.memegen.link/docs)

