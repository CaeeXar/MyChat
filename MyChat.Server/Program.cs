namespace MyChat.Server
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using MyChat.Core;

    public class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory factory = LoggerFactory.Create(b => b.AddConsole());
            ILogger logger = factory.CreateLogger("Program");

            IConfiguration configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            Thread serverThread = new Thread(() =>
            {
                Server server = new Server(
                    logger, 
                    configuration, 
                    Config.ServerIP, 
                    Config.ServerPort);

                server.Start();
            });

            serverThread.Start();


            #region Test Client
            //Thread.Sleep(1000);

            //Client client = new Client(Config.ServerIP, Config.ServerPort);
            //client.Start();
            
            //Random r = new Random();
            //for (int i = 1; i <= 5; i++)
            //{
            //    MessageRequest mr = new MessageRequest();
            //    mr.Sender = "User"+i;
            //    mr.Receiver = "<Recipient>";
            //    mr.Message = "Hello world!";
            //    client.SendRequest(mr);
            //    Thread.Sleep(1000);
            //}
            #endregion
        }
    }
}
