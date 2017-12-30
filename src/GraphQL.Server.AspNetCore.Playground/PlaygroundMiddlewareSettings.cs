using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore.Playground {

	/// <summary>
	/// The Settings of the <see cref="PlaygroundMiddleware"/>
	/// </summary>
	public class PlaygroundMiddlewareSettings {

		/// <summary>
		/// The Playground Endpoint to listen
		/// </summary>
		public PathString PlaygroundPath { get; set; } = "/playground";

		/// <summary>
		/// The GraphQL EndPoint
		/// </summary>
		public PathString GraphQLEndPoint { get; set; } = "/api/graphql";

	}

}
