using System;
using System.Security.Claims;
using GraphQL.Http;
using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GraphQL.Server.AspNetCore {

	/// <summary>
	/// The Settings of the <see cref="GraphQLMiddleware"/>
	/// </summary>
	public class GraphQLMiddlewareSettings {

		/// <summary>
		/// The GraphQL Endpoint
		/// </summary>
		public PathString EndPoint { get; set; } = "/api/graphql";

		public Func<HttpContext, ClaimsPrincipal> BuildUserContext { get; set; } = context => context.User;

		/// <summary>
		/// The Schema of the GraphQL
		/// </summary>
		public ISchema Schema { get; set; }

		public IDocumentExecuter Executer { get; set; } = new DocumentExecuter();

		public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings {
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFF'Z'",
			Formatting = Formatting.Indented
		};

		public IDocumentWriter Writer { get; set; } = new DocumentWriter();

		public ISubscriptionExecuter SubscriptionExecuter { get; set; } = new SubscriptionExecuter();

	}

}
