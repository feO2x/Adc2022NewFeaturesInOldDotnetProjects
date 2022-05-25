using System.IO;
using Newtonsoft.Json;

namespace OldApp;

#nullable enable

public static class Json
{
    public static T DeserializeFile<T>(string filePath)
    {
        var content = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(content)!;
    }
}