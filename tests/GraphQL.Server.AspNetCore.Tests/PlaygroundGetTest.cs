using Xunit;

namespace GraphQL.Server.AspNetCore.Tests {

	public class PlaygroundGetTest :BaseTest{

		[Fact]
		public async void PlaygroundGetFact() {
			var httpResponseMessage = await this.TestServer.CreateClient().GetAsync("/playground").ConfigureAwait(false);
			Assert.True(httpResponseMessage.IsSuccessStatusCode);
		}

	}

}
