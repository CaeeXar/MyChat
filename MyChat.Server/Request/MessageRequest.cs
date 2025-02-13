namespace MyChat.Server
{
    public class MessageRequest : BaseRequest
    {
        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Message { get; set; }
    }
}
