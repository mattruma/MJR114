using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net.Http;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddSingleton(sp =>
            {
                var configuration =
                    sp.GetRequiredService<IConfiguration>();

                var httpClientFactory =
                    sp.GetRequiredService<IHttpClientFactory>();

                var cosmosClientOptions =
                    new CosmosClientOptions()
                    {
                        HttpClientFactory = httpClientFactory.CreateClient
                    };

                var cosmosClient =
                    new CosmosClient(
                        configuration["APP_COSMOS_DB_CONNECTION"],
                        cosmosClientOptions);

                return cosmosClient;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebApplication1 API"
                });

                c.DocInclusionPredicate((name, api) => true);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 API v1");

                c.EnableFilter();
                c.DefaultModelsExpandDepth(-1); // Remove schema section
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
