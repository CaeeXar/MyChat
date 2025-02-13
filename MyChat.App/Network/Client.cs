namespace MyChat.App
{
    using MyChat.Core;
    using MyChat.Server;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.Json;

    public class Client
    {
        private TcpClient tcpClient;
        private StreamWriter writer;
        private StreamReader reader;

        public Client(string ip, int port)
        {
            this.tcpClient = new TcpClient(ip, port);
            var stream = tcpClient.GetStream();
            this.writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            this.reader = new StreamReader(stream, Encoding.UTF8);
        }

        public void SendRequest(BaseRequest request)
        {
            string json = JsonSerializer.Serialize(request);
            this.writer.WriteLine(json);
        }

        public void Start()
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        string? responseJson = reader.ReadLine();
                        if (string.IsNullOrEmpty(responseJson)) continue;

                        var response = JsonSerializer.Deserialize<BaseReponse>(responseJson, Config.JsonSerializerOptions);

                        if (response == null) continue;

                        if (response.StatusCode != StatusCode.OK)
                        {
                            Console.WriteLine($"[Client]: Error occured. Status-code {response.StatusCode}:");
                            Console.WriteLine(response.ErrorMessage);
                            continue;
                        }

                        if (response is BaseReponse b)
                        {
                            Console.WriteLine($"[Client]: Reponse received: " + response.StatusCode);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Client] Disconnected: {ex.Message}");
                }
                finally
                {
                    this.Stop();
                }
            });

            thread.Start();
        }

        public void Stop()
        {
            if (this.tcpClient.Connected)
            {
                this.tcpClient.Close();
            }
        }
    }


}
