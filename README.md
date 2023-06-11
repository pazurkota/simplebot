# 🤖 simplebot

Simple Discord bot to entertain your server

## Installation
### Requirements:
- [.NET 7.0 or newer](https://dotnet.microsoft.com/en-us/download)
- [API Ninjas API Key](https://api-ninjas.com/api)
- [Discord Application (bot)](https://discord.com/developers/applications)

### Bot setup:
- Create your aplication at Discord Developer Portal
- In your app, go to `Bot/Privileged Gateway` Intents and enable all settings
- Next, go to `OAuth2/URL Generator` and generate bot invite link
- Go to `OAuth2/URL Generator` and generate bot invite link
    - If you have 2FA Authentication, you will be asked to prompt a 6 digit code
- Create a JSON File in root directory:
```json
{
  "token": "Discord bot token here",
  "api-ninjas_apikey": "API Ninjas API Key here"
}
```
- Voulà! Now just run solution and everyting should be working!


## Usage
This bot uses only slash commands, so to run a command, type: `/command`

Full command list is at `/help`


## Acknowledgements
### Frameworks:

- [RestSharp](https://restsharp.dev)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [DSharpPlus](https://dsharpplus.github.io/DSharpPlus/)

### API's:
- [Excuser API](https://excuser-three.vercel.app)
- [Memegen API](https://api.memegen.link/docs)
- [Meme API](https://github.com/D3vd/Meme_Api)
- [API Ninjas](https://api-ninjas.com/api)