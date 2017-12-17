using System;
using GraphQL.Client;
using Microsoft.AspNetCore.TestHost;
using TestingWebApp;
using Xunit;

namespace GraphQL.Server.AspNetCore.Tests {

	public abstract class BaseTest : IDisposable {

		public TestServer TestServer { get;} = new TestServer(Program.BuildWebHost(null));
		public GraphQLClient GraphQLClient { get; } = new GraphQLClient(new GraphQLClientOptions {
			EndPoint=new Uri(TestServer.BaseAddress+"/api/graphql"),
			HttpClientHandler=TestServer.CreateHandler()
		});

		public void Dispose() => throw new NotImplementedException();

	}

}
