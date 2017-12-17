using GraphQL;
using GraphQL.Http;
using GraphQL.Server.AspNetCore;
using GraphQL.StarWars;
using GraphQL.StarWars.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TestingWebApp {

	public class Startup {

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services) {

			services.AddSingleton<StarWarsData>();
			services.AddSingleton<StarWarsQuery>();
			services.AddSingleton<StarWarsMutation>();
			services.AddSingleton<HumanType>();
			services.AddSingleton<HumanInputType>();
			services.AddSingleton<DroidType>();
			services.AddSingleton<CharacterInterface>();
			services.AddSingleton<EpisodeEnum>();
			services.AddSingleton<ISchema>(
				s => new StarWarsSchema(new FuncDependencyResolver(type => (GraphType)s.GetService(type))));

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
