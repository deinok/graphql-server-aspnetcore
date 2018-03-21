using System;
using System.IO;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GraphQL.Server.AspNetCore.Internal {

	internal class GraphQLHttpReader {

		private readonly GraphQLMiddlewareSettings middlewareSettings;

		public GraphQLHttpReader(GraphQLMiddlewareSettings middlewareSettings) {
			this.middlewareSettings = middlewareSettings;
		}

		public GraphQLRequest ReadRequest(HttpRequest httpRequest) {
			if (httpRequest == null) {throw new ArgumentNullException(nameof(httpRequest));}

			if (string.Equals(httpRequest.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase)) {
				return this.ReadGetRequest(httpRequest);
			}
			else if (string.Equals(httpRequest.Method, HttpMethods.Post, StringComparison.OrdinalIgnoreCase)) {
				return this.ReadPostJsonRequest(httpRequest);
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

		private GraphQLRequest ReadPostJsonRequest(HttpRequest httpRequest) {
			using (var streamReader = new StreamReader(httpRequest.Body))
			using (var jsonTextReader = new JsonTextReader(streamReader)) {
				var jsonSerializer = new JsonSerializer();
				return jsonSerializer.Deserialize<GraphQLRequest>(jsonTextReader);
			}
		}

	}
}
