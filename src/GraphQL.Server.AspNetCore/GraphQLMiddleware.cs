using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL.Server.AspNetCore.Internal;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace GraphQL.Server.AspNetCore {

	/// <summary>
	/// A middleware for GraphQL
	/// </summary>
	public class GraphQLMiddleware : BaseMiddleware {

		private readonly GraphQLMiddlewareSettings middlewareSettings;
		private readonly GraphQLHttpReader graphQLHttpReader;
		private readonly GraphQLHttpWriter graphQLHttpWriter;

		/// <summary>
		/// Create a new GraphQLMiddleware
		/// </summary>
		/// <param name="nextMiddleware">The Next Middleware</param>
		/// <param name="middlewareSettings">The Settings of the Middleware</param>
		public GraphQLMiddleware(RequestDelegate nextMiddleware, GraphQLMiddlewareSettings middlewareSettings) : base(nextMiddleware) {
			this.middlewareSettings = middlewareSettings ?? throw new ArgumentNullException(nameof(middlewareSettings));
			if (middlewareSettings.EndPoint == null) { throw new ArgumentNullException(nameof(middlewareSettings.EndPoint)); }
			if (middlewareSettings.Schema == null) { throw new ArgumentNullException(nameof(middlewareSettings.Schema)); }
			this.graphQLHttpReader = new GraphQLHttpReader(this.middlewareSettings);
			this.graphQLHttpWriter = new GraphQLHttpWriter(this.middlewareSettings);
		}

		/// <summary>
		/// Processes a GraphQLRequest
		/// </summary>
		/// <param name="httpContext">The HttpContext</param>
		/// <returns></returns>
		public override Task Invoke(HttpContext httpContext) {
			if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

			if (this.IsGraphQLApiRequest(httpContext.Request)) {
				return this.InvokeGraphQLApi(httpContext);
			}

			return this.NextMiddleware(httpContext);
		}

		private bool IsGraphQLApiRequest(HttpRequest httpRequest) {
			return httpRequest.Path.StartsWithSegments(this.middlewareSettings.EndPoint)
				&& (
					string.Equals(httpRequest.Method, HttpMethods.Post, StringComparison.OrdinalIgnoreCase)
					|| string.Equals(httpRequest.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase)
				);
		}

		private async Task InvokeGraphQLApi(HttpContext httpContext) {
			// Read the GraphQLRequest
			var graphQLRequest = this.graphQLHttpReader.ReadRequest(httpContext.Request);

			// Try Execute the request
			var executionResult = await this.middlewareSettings.Executer.ExecuteAsync(executionOptions => {
				executionOptions.Schema = this.middlewareSettings.Schema;
				executionOptions.Query = graphQLRequest.Query;
				executionOptions.OperationName = graphQLRequest.OperationName;
				if (graphQLRequest.Variables != null) { executionOptions.Inputs = ((JToken)graphQLRequest.Variables)?.ToInputs(); }
				executionOptions.UserContext = this.middlewareSettings.BuildUserContext?.Invoke(httpContext);
			});

			// Write the GraphQLResponse
			await this.graphQLHttpWriter.WriteResponseAsync(httpContext.Response, executionResult).ConfigureAwait(false);
		}


		private async Task WriteResponseAsync(HttpResponse httpResponse, ExecutionResult executionResult) {
			var json = this.middlewareSettings.Writer.Write(executionResult);

			httpResponse.ContentType = new MediaTypeHeaderValue("application/json").MediaType;

			await httpResponse.WriteAsync(json).ConfigureAwait(false);
		}

	}

}
