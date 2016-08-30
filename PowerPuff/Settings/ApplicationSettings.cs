namespace PowerPuff.Settings
{
    class ApplicationSettings : IApplicationSettings
    {
        public string Locale { get; } = System.Configuration.ConfigurationManager.AppSettings["locale"];
        public string SpeechPrimaryKey { get; } = System.Configuration.ConfigurationManager.AppSettings["speechPrimaryKey"];
        public string SpeechSecondaryKey { get; } = System.Configuration.ConfigurationManager.AppSettings["speechSecondaryKey"];
        public string LuisAppId { get; } = System.Configuration.ConfigurationManager.AppSettings["luisAppId"];
        public string LuisSubscriptionId { get; } = System.Configuration.ConfigurationManager.AppSettings["luisSubscriptionId"];
    }
}
