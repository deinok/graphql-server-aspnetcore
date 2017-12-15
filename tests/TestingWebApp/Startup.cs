using GraphQL;
using GraphQL.Http;
using GraphQL.Server.AspNetCore;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TestingWebApp.GraphQL;
using TestingWebApp.GraphQL.Types;

namespace TestingWebApp {

	public class Startup {

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services) {
			services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
			services.AddSingleton<IDocumentWriter, DocumentWriter>();

			services.AddSingleton<StarWarsData>();
			services.AddSingleton<StarWarsQuery>();
			services.AddSingleton<StarWarsMutation>();
			services.AddSingleton<HumanType>();
			services.AddSingleton<HumanInputType>();
			services.AddSingleton<DroidType>();
			services.AddSingleton<CharacterInterface>();
			services.AddSingleton<EpisodeEnum>();

			services.AddSingleton<ISchema>(s => new StarWarsSchema(new FuncDependencyResolver(type => (GraphType)s.GetService(type))));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseGraphQLServer(new GraphQLMiddlewareSettings {
				Schema = app.ApplicationServices.GetRequiredService<ISchema>()
			});

			app.Run(async (context) => {
				await context.Response.WriteAsync("Hello World!");
			});
		}

	}

}
