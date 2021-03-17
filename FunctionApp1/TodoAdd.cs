using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public static class TodoAdd
    {
        [FunctionName(nameof(TodoAdd))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos")] TodoAddOptions todoAddOptions,
            [CosmosDB(
                databaseName: "todo",
                collectionName: "todos",
                ConnectionStringSetting = "APP_COSMOS_DB_CONNECTION")]
                IAsyncCollector<Todo> todos,
            ILogger logger)
        {
            logger.LogInformation($"{nameof(TodoAdd)} function processed a request.");

            var todo = new Todo
            {
                Description = todoAddOptions.Description
            };

            await todos.AddAsync(todo);

            return new CreatedAtRouteResult($"todos/{todo.Id}", todo);
        }
    }
}

