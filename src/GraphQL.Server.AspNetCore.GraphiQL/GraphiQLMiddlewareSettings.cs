using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore.GraphiQL {

	public class GraphiQLMiddlewareSettings {

		public PathString GraphiQLPath { get; set; } = "/graphiql";

		public PathString GraphQLPath { get; set; } = "/api/graphql";

	}

}
