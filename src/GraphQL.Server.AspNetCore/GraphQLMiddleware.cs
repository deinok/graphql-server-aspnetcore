using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphQL.Server.AspNetCore {

	/// <summary>
	/// A middleware for GraphQL
	/// </summary>
	public class GraphQLMiddleware : BaseMiddleware {

		private readonly GraphQLMiddlewareSettings middlewareSettings;

		/// <summary>
		/// Create a new GraphQLMiddleware
		/// </summary>
		/// <param name="nextMiddleware">The Next Middleware</param>
		/// <param name="middlewareSettings">The Settings of the Middleware</param>
		public GraphQLMiddleware(RequestDelegate nextMiddleware, GraphQLMiddlewareSettings middlewareSettings) : base(nextMiddleware) {
			this.middlewareSettings = middlewareSettings ?? throw new ArgumentNullException(nameof(middlewareSettings));
			if (middlewareSettings.EndPoint == null) { throw new ArgumentNullException(nameof(middlewareSettings.EndPoint)); }
			if (middlewareSettings.Schema == null) { throw new ArgumentNullException(nameof(middlewareSettings.Schema)); }
		}

		/// <summary>
		/// Processes a GraphQLRequest
		/// </summary>
		/// <param name="httpContext">The HttpContext</param>
		/// <returns></returns>
		public async override Task Invoke(HttpContext httpContext) {
			if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

			if (this.IsGraphQLApiRequest(httpContext.Request)) {
				await this.InvokeGraphQLApi(httpContext).ConfigureAwait(false);
				return;
			}

			await this.NextMiddleware(httpContext).ConfigureAwait(false);
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
			var request = await this.ReadRequestAsync(httpContext.Request).ConfigureAwait(false);

			// Try Execute the request
			var result = await this.middlewareSettings.Executer.ExecuteAsync(executionOptions => {
				executionOptions.Schema = this.middlewareSettings.Schema;
				executionOptions.Query = request.Query;
				executionOptions.OperationName = request.OperationName;
				if (request.Variables != null) { executionOptions.Inputs = ((JToken)request.Variables)?.ToInputs(); }
				executionOptions.UserContext = this.middlewareSettings.BuildUserContext?.Invoke(httpContext);
			});

			// Write the GraphQLResponse
			await WriteResponseAsync(httpContext.Response, result).ConfigureAwait(false);
		}


		private async Task<GraphQLRequest> ReadRequestAsync(HttpRequest httpRequest) {
			if (string.Equals(httpRequest.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase)) {
				return this.ReadGetRequest(httpRequest);
			} else if (string.Equals(httpRequest.Method, HttpMethods.Post, StringComparison.OrdinalIgnoreCase)) {
				return await this.ReadPostRequestAsync(httpRequest).ConfigureAwait(false);
			}
			throw new InvalidOperationException();
		}

		private GraphQLRequest ReadGetRequest(HttpRequest httpRequest) {
			return new GraphQLRequest {
				Query = httpRequest.Query["query"],
				OperationName = httpRequest.Query["operationName"],
				Variables = httpRequest.Query.ContainsKey("variables") ? JsonConvert.DeserializeObject(httpRequest.Query["variables"]) : null
			};
		}

		private async Task<GraphQLRequest> ReadPostRequestAsync(HttpRequest httpRequest) {
			using (var streamReader = new StreamReader(httpRequest.Body)) {
				var body = await streamReader.ReadToEndAsync().ConfigureAwait(true);
				return JsonConvert.DeserializeObject<GraphQLRequest>(body);
			}
		}

		private async Task WriteResponseAsync(HttpResponse httpResponse, ExecutionResult executionResult) {
			var json = this.middlewareSettings.Writer.Write(executionResult);

			httpResponse.ContentType = new MediaTypeHeaderValue("application/json").MediaType;

			await httpResponse.WriteAsync(json).ConfigureAwait(false);
		}

	}

	internal static class GraphQLServiceExtensions {

		public static Inputs ToInputs(this JToken obj) {
			var variables = obj.GetValue() as Dictionary<string, object> ?? new Dictionary<string, object>();
			return new Inputs(variables);
		}

	}

}
