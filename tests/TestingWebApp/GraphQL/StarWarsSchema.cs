using GraphQL;
using GraphQL.Types;

namespace TestingWebApp.GraphQL {

	public class StarWarsSchema : Schema {

		public StarWarsSchema(IDependencyResolver resolver): base(resolver) {
			this.Query = resolver.Resolve<StarWarsQuery>();
			this.Mutation = resolver.Resolve<StarWarsMutation>();
		}

	}

}
