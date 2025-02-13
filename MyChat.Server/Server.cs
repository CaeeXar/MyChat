namespace MyChat.Server
{
    using System.Net.Sockets;
    using System.Net;
    using System.Text;
    using System.Text.Json;
    using Microsoft.Extensions.Logging;
    using MyChat.Core;
    using Microsoft.Extensions.Configuration;

    class Server
    {
        private TcpListener listener;
        private Dictionary<Guid, object?> clients;
        private ILogger logger;
        private IConfiguration configuration;

        public Server(ILogger logger, IConfiguration configuration, string address, int port)
        {
            this.logger = logger;
            this.configuration = configuration;
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

            try
            {
                while (true)
                {
                    string requestJson = reader.ReadLine()!;
                    var request = JsonSerializer.Deserialize<BaseRequest>(requestJson, Config.JsonSerializerOptions);

                    if (request is MessageRequest mr)
                    {
                        this.logger.LogInformation($"MessageRequest received from {client.GID}");
                        //Console.WriteLine($"Message from {mr?.Sender} to {mr?.Receiver}: {mr?.Message}");
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
