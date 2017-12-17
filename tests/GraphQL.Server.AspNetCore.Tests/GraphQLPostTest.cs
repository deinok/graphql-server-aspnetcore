using Xunit;

namespace GraphQL.Server.AspNetCore.Tests {

	public class GraphQLPostTest : BaseTest {

		[Fact]
		public async void QueryHeroPostFact() {
			var graphQLResponse = await this.GraphQLClient.PostQueryAsync("{hero{name}}").ConfigureAwait(false);
			Assert.NotNull(graphQLResponse.Data);
			Assert.Null(graphQLResponse.Errors);
		}

	}

}
