using System;
using System.IO;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GraphQL.Server.AspNetCore.Internal {

	internal static class GraphQLHttpReader {

		public static GraphQLRequest ReadRequest(HttpRequest httpRequest) {
			if (httpRequest == null) {throw new ArgumentNullException(nameof(httpRequest));}

			if (string.Equals(httpRequest.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase)) {
				return ReadGetRequest(httpRequest);
			}
			else if (string.Equals(httpRequest.Method, HttpMethods.Post, StringComparison.OrdinalIgnoreCase)) {
				return ReadPostJsonRequest(httpRequest);
			}
			throw new InvalidOperationException();
		}

		private static GraphQLRequest ReadGetRequest(HttpRequest httpRequest) {
			return new GraphQLRequest {
				Query = httpRequest.Query["query"],
				OperationName = httpRequest.Query["operationName"],
				Variables = httpRequest.Query.ContainsKey("variables") ? JsonConvert.DeserializeObject(httpRequest.Query["variables"]) : null
			};
		}

		private static GraphQLRequest ReadPostJsonRequest(HttpRequest httpRequest) {
			using (var streamReader = new StreamReader(httpRequest.Body))
			using (var jsonTextReader = new JsonTextReader(streamReader)) {
				var jsonSerializer = new JsonSerializer();
				return jsonSerializer.Deserialize<GraphQLRequest>(jsonTextReader);
			}
		}

	}
}
