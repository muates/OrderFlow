using System.Text;
using System.Text.Json;

namespace SharedLibrary.Helper;

public static class JsonSerializerHelper
{
    public static byte[] Serialize<T>(T obj)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
    }
}