using System.Text.Json;
using System.Text.Json.Serialization;

namespace Restful.Data
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            string allData = File.ReadAllText(@"../../../ApiSettings.json");

            Settings = JsonSerializer.Deserialize<Settings>(allData); 
        }

        public Settings Settings { get; set; }
    }
}
