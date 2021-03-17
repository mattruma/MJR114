using Newtonsoft.Json;
using System;

namespace FunctionApp1
{
    public class Todo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("todoId")]
        public string TodoId { get; set; }

        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }

        public Todo()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsCompleted = false;
            this.TodoId = this.Id;
        }
    }
}
