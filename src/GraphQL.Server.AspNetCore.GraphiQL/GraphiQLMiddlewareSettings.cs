using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore.GraphiQL {

	/// <summary>
	/// The Settings of the <see cref="GraphiQLMiddleware"/>
	/// </summary>
	public class GraphiQLMiddlewareSettings {

		/// <summary>
		/// The GraphiQL Endpoint to listen
		/// </summary>
		public PathString GraphiQLPath { get; set; } = "/graphiql";

		/// <summary>
		/// The GraphQL EndPoint
		/// </summary>
		public PathString GraphQLEndPoint { get; set; } = "/api/graphql";

	}

}
