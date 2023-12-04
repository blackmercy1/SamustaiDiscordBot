using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
        private FileDownloaderBootstraper _fileDownloader;
        private string _token = "MTE4MDIwNzkxNzg4MjkzMzMwOA.GrWS6Z.Mg-J5gBxCwwTI-QpCuAaeZ7Yt3PnT5oKJldgCg";
    
        private async Task MainAsync()
        {
            _fileDownloader = new FileDownloaderBootstraper();
            _client = new DiscordSocketClient();
            
            _client.MessageReceived += HandleCommandAsync;
            _client.Log += Log; 
    
            await _client.LoginAsync(TokenType.Bot, _token);
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
           
            _fileDownloader.DownloadFile(ExtractLink(messageParam.Content));
            messageParam.Channel.SendMessageAsync("Downloading");

            return Task.CompletedTask;
        }

        private string ExtractLink(string content)
        {
            Match url = Regex.Match(content, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            string finalUrl = url.ToString();

            return finalUrl;
        }
    }

    public class FileDownloaderBootstraper
    {
        public void DownloadFile(string linkToDownload)
        {
            var fileDownloader = new FileDownloader();
            var filename = fileDownloader.GetFileName(linkToDownload);
            var filePath = fileDownloader.CrateFileWithPath( @"/Users/black_mercy/Downloads", filename);
            
            fileDownloader.DownloadProgressChanged += ( sender, e ) => Console.WriteLine( "Progress changed " + e.BytesReceived + " " + e.TotalBytesToReceive );
        
            fileDownloader.DownloadFileCompleted += ( sender, e ) =>
            {
                if( e.Cancelled )
                    Console.WriteLine( "Download cancelled" );
                else if( e.Error != null )
                    Console.WriteLine( "Download failed: " + e.Error );
                else
                    Console.WriteLine( "Download completed" );
            };

            fileDownloader.DownloadFileAsync(
                linkToDownload,
                @filePath);

            Console.ReadLine();
        }
    }
}