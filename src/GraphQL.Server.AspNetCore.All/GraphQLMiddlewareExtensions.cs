using System;
using GraphQL.Server.AspNetCore;
using GraphQL.Server.AspNetCore.GraphiQL;
using GraphQL.Server.AspNetCore.Playground;

namespace Microsoft.AspNetCore.Builder {

	/// <summary>
	/// Extension methods for all <see cref="GraphQLMiddleware"/>
	/// </summary>
	public static class GraphQLMiddlewareExtensions {

		/// <summary>
		/// Enables a GraphQL using the specified settings
		/// </summary>
		/// <param name="applicationBuilder"></param>
		/// <param name="graphQLMiddlewareSettings">The settings of the <see cref="GraphQLMiddleware"/></param>
		/// <returns></returns>
		public static IApplicationBuilder UseGraphQL(this IApplicationBuilder applicationBuilder, GraphQLMiddlewareSettings graphQLMiddlewareSettings) =>
			applicationBuilder.UseGraphQL(graphQLMiddlewareSettings, new GraphiQLMiddlewareSettings(), new PlaygroundMiddlewareSettings());

		/// <summary>
		/// Enables a GraphQL using the specified settings
		/// </summary>
		/// <param name="applicationBuilder"></param>
		/// <param name="graphQLMiddlewareSettings">The settings of the <see cref="GraphQLMiddleware"/></param>
		/// <param name="graphiQLMiddlewareSettings">The settings of the <see cref="GraphiQLMiddleware"/></param>
		/// <param name="playgroundMiddlewareSettings">The settings of the <see cref="PlaygroundMiddleware"/></param>
		/// <returns></returns>
		public static IApplicationBuilder UseGraphQL(this IApplicationBuilder applicationBuilder, GraphQLMiddlewareSettings graphQLMiddlewareSettings, GraphiQLMiddlewareSettings graphiQLMiddlewareSettings, PlaygroundMiddlewareSettings playgroundMiddlewareSettings) {
			if (graphQLMiddlewareSettings == null) { throw new ArgumentNullException(nameof(graphQLMiddlewareSettings)); }
			if (graphiQLMiddlewareSettings == null) { throw new ArgumentNullException(nameof(graphiQLMiddlewareSettings)); }
			if (playgroundMiddlewareSettings == null) { throw new ArgumentNullException(nameof(playgroundMiddlewareSettings)); }

			applicationBuilder.UseGraphQLServer(graphQLMiddlewareSettings);
			applicationBuilder.UseGraphiQLServer(graphiQLMiddlewareSettings);
			applicationBuilder.UsePlaygroundServer(playgroundMiddlewareSettings);
			return applicationBuilder;
		}

	}

}
