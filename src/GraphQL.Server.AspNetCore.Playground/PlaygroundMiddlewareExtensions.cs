using System;
using Microsoft.AspNetCore.Builder;

namespace GraphQL.Server.AspNetCore.Playground {

	/// <summary>
	/// Extension methods for <see cref="PlaygroundMiddleware"/>
	/// </summary>
	public static class PlaygroundMiddlewareExtensions {

		/// <summary>
		/// Enables a PlaygroundServer using the specified settings
		/// </summary>
		/// <param name="applicationBuilder"></param>
		/// <param name="settings">The settings of the Middleware</param>
		/// <returns></returns>
		public static IApplicationBuilder UsePlaygroundServer(this IApplicationBuilder applicationBuilder, PlaygroundMiddlewareSettings settings) {
			if (settings == null) { throw new ArgumentNullException(nameof(settings)); }

			applicationBuilder.UseMiddleware<PlaygroundMiddleware>(settings);
			return applicationBuilder;
		}

	}

}
