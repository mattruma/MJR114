using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public static class TodoFetchById
    {
        [FunctionName(nameof(TodoFetchById))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "todo",
                collectionName: "todos",
                ConnectionStringSetting = "APP_COSMOS_DB_CONNECTION",
                Id = "{id}",
                PartitionKey = "{id}")] Todo todo,
            ILogger logger)
        {
            logger.LogInformation($"{nameof(TodoFetchById)} function processed a request.");

            if (todo == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(todo);
        }
    }
}

