using System;
using GraphQL.Client;
using Microsoft.AspNetCore.TestHost;
using TestingWebApp;

namespace GraphQL.Server.AspNetCore.Tests {

	public abstract class BaseTest : IDisposable {

		protected TestServer TestServer { get; }
		protected GraphQLClient GraphQLClient { get; } 

		public BaseTest() {
			this.TestServer = new TestServer(Program.BuildWebHost(null));
			this.GraphQLClient = new GraphQLClient(new GraphQLClientOptions {
				EndPoint = new Uri(TestServer.BaseAddress + "/api/graphql"),
				HttpMessageHandler = TestServer.CreateHandler()
			});
		}

		public void Dispose() {
			this.GraphQLClient.Dispose();
			this.TestServer.Dispose();
		}

	}

}
