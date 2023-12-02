using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SamustaiDiscordBot
{
    public class Program
    {
        private static void Main() => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            
            _client.MessageReceived += HandleCommandAsync;
            _client.Log += Log; 
            
            var token = "MTE4MDIwNzkxNzg4MjkzMzMwOA.G0G7xS.VnKewgrBahWVPB1UjxFfqoAhN-uTRnaqe2ItIY";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private Task HandleCommandAsync(SocketMessage messageParam)
        {
            if (messageParam.Author.IsBot)
                return Task.CompletedTask;
            messageParam.Channel.SendMessageAsync(messageParam.Content);
            return Task.CompletedTask;
        }
    }
}