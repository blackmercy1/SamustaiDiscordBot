using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SamustaiDiscordBot
{
    // public class Program
    // {
    //     private static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
    //
    //     
    //     private DiscordSocketClient _client;
    //
    //     public async Task MainAsync()
    //     {
    //         _client = new DiscordSocketClient();
    //         WorkWithFile();
    //         
    //         _client.MessageReceived += HandleCommandAsync;
    //         _client.Log += Log; 
    //         
    //         var token = "/MTE4MDIwNzkxNzg4MjkzMzMwOA.G027oy.mtABZJxqUuF18GdOzsUK_C_wycF0sSxAU1ODlk";
    //
    //         await _client.LoginAsync(TokenType.Bot, token);
    //         await _client.StartAsync();
    //         
    //         await Task.Delay(-1);
    //     }
    //
    //     private void WorkWithFile()
    //     {
    //         var credentialsPath =
    //             "Credentials.json";
    //     }
    //
    //     private Task Log(LogMessage arg)
    //     {
    //         Console.WriteLine(arg);
    //         return Task.CompletedTask;
    //     }
    //
    //     private Task HandleCommandAsync(SocketMessage messageParam)
    //     {
    //         if (messageParam.Author.IsBot)
    //             return Task.CompletedTask;
    //         messageParam.Channel.SendMessageAsync(messageParam.Content);
    //         return Task.CompletedTask;
    //     }
    // }

    public class Program
    {
        private static void Main(string[] args)
        {
            var fileDownloader = new FileDownloader();
    
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

            var linkToDownload = "https://drive.google.com/file/d/1e48SnRiRDn8HIVPuf_DTOO3HGPz8rM-L/view?usp=sharing";
            var filename = fileDownloader.GetFileName(linkToDownload);
            var filePath = fileDownloader.CrateFileWithPath("/Users/black_mercy/Downloads", filename);
            fileDownloader.DownloadFileAsync( 
                "https://drive.google.com/file/d/1e48SnRiRDn8HIVPuf_DTOO3HGPz8rM-L/view?usp=sharing",
                filePath );

            Console.ReadLine();
        }
    }
}