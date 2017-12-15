using GraphQL.Server.AspNetCore;
using GraphQL.StarWars;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TestingWebApp {

	public class Startup {

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services) {
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseGraphQLServer(new GraphQLMiddlewareSettings {
				Schema = new StarWarsSchema()
			});

			app.Run(async (context) => {
				await context.Response.WriteAsync("Hello World!");
			});
		}

	}

}
