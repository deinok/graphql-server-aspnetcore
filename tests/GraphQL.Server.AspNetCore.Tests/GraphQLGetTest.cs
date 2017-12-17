using Xunit;

namespace GraphQL.Server.AspNetCore.Tests {

	public class GraphQLGetTest:BaseTest {

		[Fact]
		public async void QueryHeroGetFact() {
			var graphQLResponse=await this.GraphQLClient.GetQueryAsync("{hero{name}}").ConfigureAwait(false);
			Assert.NotNull(graphQLResponse.Data);
			Assert.Null(graphQLResponse.Errors);
		}

	}

}
