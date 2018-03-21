using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore.Internal {

	internal class GraphQLHttpWriter {

		private readonly GraphQLMiddlewareSettings middlewareSettings;

		public GraphQLHttpWriter(GraphQLMiddlewareSettings middlewareSettings) {
			this.middlewareSettings = middlewareSettings;
		}

		public async Task WriteResponseAsync(HttpResponse httpResponse, ExecutionResult executionResult) {
			var json = this.middlewareSettings.Writer.Write(executionResult);

			httpResponse.ContentType = "application/json";

			await httpResponse.WriteAsync(json).ConfigureAwait(false);
		}

	}

}
