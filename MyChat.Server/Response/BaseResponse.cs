namespace MyChat.Server
{
    //using System.Text.Json.Serialization;

    //[JsonDerivedType(typeof(MessageRequest), typeDiscriminator: "MessageRequest")]
    public class BaseReponse
    {
        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
