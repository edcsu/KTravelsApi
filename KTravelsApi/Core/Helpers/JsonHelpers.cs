using System.Text.Json;
using System.Text.Json.Serialization;

namespace KTravelsApi.Core.Helpers;

public static class JsonHelpers
{
    public static JsonSerializerOptions GetSerializerSettings()
    {
        var settings = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        settings.Converters.Add(new JsonStringEnumConverter());
        return settings;
    }
}