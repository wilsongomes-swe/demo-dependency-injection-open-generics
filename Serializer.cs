using System.Text.Json;

namespace OpenGenericsDI;

public interface ISerializer<T>
{
    string Serialize(T obj);
}

public class Serializer<T> : ISerializer<T>
{
    public string Serialize(T obj) => JsonSerializer.Serialize(obj, new JsonSerializerOptions()
    {
        PropertyNamingPolicy = new SnakeCaseNamingPolicy()
    });
}

public class UpperCaseSerializer<T> : ISerializer<T>
{
    public string Serialize(T obj) => JsonSerializer.Serialize(obj, new JsonSerializerOptions()
    {
        PropertyNamingPolicy = new UpperCaseNamingPolicy()
    });
}

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

    public override string ConvertName(string name)
    {
        return string.Concat(
            name.Select(
                (x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()
        )).ToLower(); ;
    }
}

public class UpperCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

    public override string ConvertName(string name) => name.ToUpper();
}