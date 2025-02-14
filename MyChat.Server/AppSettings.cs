namespace MyChat.Server
{ 
    using Microsoft.Extensions.Configuration;
    using System.Runtime.CompilerServices;

    public class AppSettings
    {
        private static readonly IConfiguration Configuration;

        static AppSettings()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        /// <summary>
        /// Returns the configured setting of given path.
        /// </summary>
        /// <typeparam name="T">The type of the setting.</typeparam>
        /// <param name="path">The path of the setting.</param>
        /// <returns>The configured setting. Null if not found.</returns>
        public static object? GetSetting<T>(string path)
        {
            try
            {
                T? value = Configuration.GetValue<T>(path);
                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string? GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name);
        }
    }
}
