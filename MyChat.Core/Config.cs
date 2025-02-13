using System.Text.Json;

namespace MyChat.Core
{
    public static class Config
    {
        public static string ServerIP = "127.0.0.1";
        public static int ServerPort = 5000;
        public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }
}
