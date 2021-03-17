using Newtonsoft.Json;

namespace WebApplication1
{
    public class TodoAddOptions
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
