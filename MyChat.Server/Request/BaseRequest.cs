namespace MyChat.Server
{
    using System.Text.Json.Serialization;

    [JsonDerivedType(typeof(MessageRequest), typeDiscriminator: "MessageRequest")]
    public class BaseRequest
    { 
    }
}
