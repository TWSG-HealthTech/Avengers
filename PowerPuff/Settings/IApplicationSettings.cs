namespace PowerPuff.Settings
{
    interface IApplicationSettings
    {
        string Locale { get; }
        string SpeechPrimaryKey { get; }
        string SpeechSecondaryKey { get; }
        string LuisAppId { get; }
        string LuisSubscriptionId { get; }
    }
}
