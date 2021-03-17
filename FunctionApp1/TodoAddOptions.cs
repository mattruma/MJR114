using Newtonsoft.Json;

namespace FunctionApp1
{
    public class TodoAddOptions
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
