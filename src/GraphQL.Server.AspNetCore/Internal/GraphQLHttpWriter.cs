using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GraphQL.Server.AspNetCore.Internal {

	internal class GraphQLHttpWriter {

		private readonly GraphQLMiddlewareSettings middlewareSettings;

		public GraphQLHttpWriter(GraphQLMiddlewareSettings middlewareSettings) {
			this.middlewareSettings = middlewareSettings;
		}

		public async Task WriteResponseAsync(HttpContext httpContext, ExecutionResult executionResult) {
			httpContext.Response.ContentType = "application/json";
			var jsonResult = JsonConvert.SerializeObject(executionResult, this.middlewareSettings.JsonSerializerSettings);
			await httpContext.Response.WriteAsync(jsonResult).ConfigureAwait(false);
		}

	}

}
