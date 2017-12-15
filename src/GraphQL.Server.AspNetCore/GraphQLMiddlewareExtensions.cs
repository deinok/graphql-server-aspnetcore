using System;
using Microsoft.AspNetCore.Builder;

namespace GraphQL.Server.AspNetCore {

	/// <summary>
	/// Extension methods for <see cref="GraphQLMiddleware"/>
	/// </summary>
	public static class GraphQLMiddlewareExtensions {

		/// <summary>
		/// Enables a GraphQLServer using the specified settings
		/// </summary>
		/// <param name="applicationBuilder"></param>
		/// <param name="graphQLSettings">The settings of the Middleware</param>
		/// <returns></returns>
		public static IApplicationBuilder UseGraphQLServer(this IApplicationBuilder applicationBuilder, GraphQLMiddlewareSettings graphQLSettings) {
			if (graphQLSettings == null) { throw new ArgumentNullException(nameof(graphQLSettings)); }

			applicationBuilder.UseMiddleware<GraphQLMiddleware>(graphQLSettings);
			return applicationBuilder;
		}

	}

}
