
# 🤖 simplebot

Simple Discord bot to entertain your server
## Bot Setup
### Requirements:
 - [.NET 7.0 or newer](https://dotnet.microsoft.com/en-us/download).
 - [API Ninjas API Key](https://api-ninjas.com/api)
 - [Discord Application (bot)](https://discord.com/developers/applications)

### Setting up bot:
- Create your aplication at Discord Developer Portal ![Image 1](https://cdn.discordapp.com/attachments/969140752766631969/1112370145889894400/ss1.png)

- In your app, go to `Bot/Privileged Gateway Intents` and enable all settings ![Image 2](https://cdn.discordapp.com/attachments/969140752766631969/1112370606256705617/ss2.png)

- Next, go to `OAuth2/URL Generator` and generate bot invite link ![Image 3](https://cdn.discordapp.com/attachments/969140752766631969/1112371418689183794/ss3.png) ![Image 4](https://cdn.discordapp.com/attachments/969140752766631969/1112371838278963230/ss4.png)

- And here we go! We now created and configured your bot!

### Getting Bot Token
####  To get bot working, we need bot token
- To get one, go to `Bot/Reset Token` and click `Reset Token`: ![Image 5](https://cdn.discordapp.com/attachments/969140752766631969/1112373709311852654/ss5.png)
- If you have 2FA Authentication, you will be asked to prompt a 6 digit code
- Voulà! Now we have Discord Bot Token!

### Configurating `config.json`:
- If you have bot token and API Ninja Api Key, create `config.json` file in root directory (where is `Program.cs` file)
```json
{
  "token": "Discord bot token here",
  "prefix": ">",
  "api-ninjas_apikey": "API Ninjas API Key here"
}
```

    
## Acknowledgements

 - [RestSharp](https://restsharp.dev)
 - [Newtonsoft.Json](https://www.newtonsoft.com/json)
 - [DSharpPlus](https://dsharpplus.github.io/DSharpPlus/)
 - [Excuser API](https://excuser-three.vercel.app)

