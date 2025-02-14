namespace MyChat.Server
{
    using System.Net.Sockets;
    using System.Net;
    using System.Text;
    using System.Text.Json;
    using Microsoft.Extensions.Logging;
    using MyChat.Core;
    using MyChat.App.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using MyChat.Server.Service;
    using MyChat.Server.Repository;

    class Server
    {
        private TcpListener listener;
        private Dictionary<Guid, object?> clients;
        private ILogger logger;
        private DbContextOptions<AppDbContext> dbContextOptions;

        public Server(
            ILogger logger, 
            DbContextOptions<AppDbContext> dbContextOptions, 
            string address, 
            int port)
        {
            Console.WriteLine("server nig");
            this.logger = logger;
            this.dbContextOptions = dbContextOptions;
            this.listener = new TcpListener(IPAddress.Parse(address), port);
            this.clients = new Dictionary<Guid, object?>();
        }

        public void Start()
        {
            this.listener.Start();
            this.logger.LogInformation($"Server is now running and listening on {this.listener.LocalEndpoint}...");

            while (true)
            {
                var tcpClient = this.listener.AcceptTcpClient();

                Guid guid = Guid.NewGuid();
                ClientData client = new ClientData(tcpClient, guid);
                this.clients.Add(guid, client);

                this.logger.LogInformation($"New client connected {guid}");

                ThreadPool.QueueUserWorkItem(HandleClient, client);
            }
        }

        private void HandleClient(object? obj)
        {
            if (obj == null) return;

            var client = (ClientData)obj;
            var stream = client.TcpClient.GetStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            //UserService userService = new UserService(new UserRepository(this.dbContextOptions)); TODO

            try
            {
                while (true)
                {
                    string requestJson = reader.ReadLine()!;
                    var request = JsonSerializer.Deserialize<BaseRequest>(requestJson, Config.JsonSerializerOptions);

                    if (request is MessageRequest mr)
                    {
                        this.logger.LogInformation($"MessageRequest received from {client.GID}");
                        var response = new BaseReponse()
                        {
                            StatusCode = StatusCode.OK,
                        };
                        
                        writer.WriteLine(JsonSerializer.Serialize(response));
                    }
                    else if (request is BaseRequest)
                    {
                        this.logger.LogInformation($"BaseRequest received from {client.GID}");
                    }
                    else
                    {
                        this.logger.LogWarning("Unknown request received!");
                    }
                }
            }
            catch (Exception e)
            {
                this.logger.LogError("Server internal error occured!");
                this.logger.LogError($"{e.Message}");
            }
            finally
            {
                client.TcpClient.Close();
            }
        }
    }
}
