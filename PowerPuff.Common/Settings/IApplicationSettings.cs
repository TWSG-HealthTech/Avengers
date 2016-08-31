namespace PowerPuff.Common.Settings
{
    public interface IApplicationSettings
    {
        string Locale { get; }
        string SpeechPrimaryKey { get; }
        string SpeechSecondaryKey { get; }
        string LuisAppId { get; }
        string LuisSubscriptionId { get; }
        string ServerUrl { get; }
    }
}
