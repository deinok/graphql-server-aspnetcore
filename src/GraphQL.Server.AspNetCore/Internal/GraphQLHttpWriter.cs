using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore.Internal {

	internal class GraphQLHttpWriter {

		private readonly GraphQLMiddlewareSettings middlewareSettings;

		public GraphQLHttpWriter(GraphQLMiddlewareSettings middlewareSettings) {
			this.middlewareSettings = middlewareSettings;
		}

		public async Task WriteResponseAsync(HttpContext httpContext, ExecutionResult executionResult) {
			var json = this.middlewareSettings.Writer.Write(executionResult);

			httpContext.Response.ContentType = "application/json";

			await httpContext.Response.WriteAsync(json).ConfigureAwait(false);
		}

	}

}
