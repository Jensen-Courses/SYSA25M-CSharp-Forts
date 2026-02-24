using System.Text.Encodings.Web;
using System.Text.Json;

namespace simple_api.Data;

public class Storage<T>
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public static List<T> ReadJson(string path)
    {
        var json = File.ReadAllText(path);
        var result = JsonSerializer.Deserialize<List<T>>(json, _options);
        return result;
    }

    public static void WriteJson(string path, List<T> data)
    {
        var json = JsonSerializer.Serialize(data, _options);
        File.WriteAllText(path, json);
    }
}
