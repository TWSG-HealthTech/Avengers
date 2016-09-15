using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerPuff.Common.Helpers
{
    public class JsonFileUtils
    {
        public static async Task<T> ReadFromJsonFile<T>(string filePath) where T : new()
        {
            using (var reader = new StreamReader(filePath))
            {
                return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
            }
        }

        public static Task WriteToJsonFile<T>(string filePath, T content) where T : new()
        {
            using (var writer = new StreamWriter(filePath))
            {
                return writer.WriteAsync(JsonConvert.SerializeObject(content));
            }
        }
    }
}