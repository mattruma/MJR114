using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApplication1
{
    [ApiController]
    [Route("api/v1/todos")]
    public class TodoAdd : ControllerBase
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<TodoAdd> _logger;

        public TodoAdd(
            CosmosClient cosmosClient,
            ILogger<TodoAdd> logger)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Run(
            TodoAddOptions todoAddOptions)
        {
            var todos =
                _cosmosClient.GetContainer("todo", "todos");

            _logger.LogInformation($"{nameof(TodoAdd)} function processed a request.");

            var todo = new Todo
            {
                Description = todoAddOptions.Description
            };

            await todos.CreateItemAsync(todo, new PartitionKey(todo.TodoId));

            return new CreatedAtRouteResult($"todos/{todo.Id}", todo);
        }
    }
}
