using System;
using System.Security.Claims;
using GraphQL.Http;
using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore {

	/// <summary>
	/// The Settings of the <see cref="GraphQLMiddleware"/>
	/// </summary>
	public class GraphQLMiddlewareSettings {

		/// <summary>
		/// The GraphQL Endpoint
		/// </summary>
		public PathString EndPoint { get; set; } = "/api/graphql";

		public Func<HttpContext, ClaimsPrincipal> BuildUserContext { get; set; }

		/// <summary>
		/// The Schema of the GraphQL
		/// </summary>
		public ISchema Schema { get; set; }

		public IDocumentExecuter Executer { get; set; } = new DocumentExecuter();

		public IDocumentWriter Writer { get; set; } = new DocumentWriter();

		public ISubscriptionExecuter SubscriptionExecuter { get; set; } = new SubscriptionExecuter();

	}

}
