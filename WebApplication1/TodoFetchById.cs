using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApplication1
{
    [ApiController]
    [Route("api/v1/todos")]
    public class TodoFetchById : ControllerBase
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<TodoAdd> _logger;

        public TodoFetchById(
            CosmosClient cosmosClient,
            ILogger<TodoAdd> logger)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Run(
            [FromRoute] string id)
        {
            var todos =
                _cosmosClient.GetContainer("todo", "todos");

            _logger.LogInformation($"{nameof(TodoFetchById)} function processed a request.");

            var todoResponse =
                await todos.ReadItemAsync<Todo>(id, new PartitionKey(id));

            var todo =
                todoResponse.Resource;

            return new OkObjectResult(todo);
        }
    }
}
