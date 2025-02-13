namespace MyChat.App
{
    using System.Net.Sockets;

    class Session
    {
        private readonly TcpClient client;

        public event Action? ClientConnected;

        public Session()
        {
            this.client = new TcpClient();
        }

        public void Connect()
        {
            if (this.client.Connected)
            {
                return;
            }
        }

    }
}
