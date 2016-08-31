using System.Configuration;

namespace PowerPuff.Common.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string Locale { get; } = ConfigurationManager.AppSettings["locale"];
        public string SpeechPrimaryKey { get; } = ConfigurationManager.AppSettings["speechPrimaryKey"];
        public string SpeechSecondaryKey { get; } = ConfigurationManager.AppSettings["speechSecondaryKey"];
        public string LuisAppId { get; } = ConfigurationManager.AppSettings["luisAppId"];
        public string LuisSubscriptionId { get; } = ConfigurationManager.AppSettings["luisSubscriptionId"];
        public string ServerUrl { get; } = ConfigurationManager.AppSettings["serverUrl"];
    }
}
