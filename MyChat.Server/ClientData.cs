namespace MyChat.Server
{
    using System.Net.Sockets;

    public class ClientData
    {
        public ClientData(TcpClient client, Guid gid)
        {
            this.TcpClient = client;
            this.GID = gid;
        }

        public TcpClient TcpClient { get; private set; }

        public Guid GID { get; private set; }

        public string? Username { get; set; }
    }
}
