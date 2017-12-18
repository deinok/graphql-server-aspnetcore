using Xunit;

namespace GraphQL.Server.AspNetCore.Tests {

	public class GraphiQLGetTest :BaseTest{

		[Fact]
		public async void GraphiQLGetFact() {
			var httpResponseMessage = await this.TestServer.CreateClient().GetAsync("/graphiql").ConfigureAwait(false);
			Assert.True(httpResponseMessage.IsSuccessStatusCode);
		}

	}

}
