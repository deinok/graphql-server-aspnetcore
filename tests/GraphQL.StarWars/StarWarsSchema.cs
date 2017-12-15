using GraphQL.Types;

namespace GraphQL.StarWars {

	public class StarWarsSchema : Schema {

		private readonly StarWarsData starWarsData = new StarWarsData();

		public StarWarsSchema(): base() {
			this.Query = new StarWarsQuery(starWarsData);
			this.Mutation = new StarWarsMutation(starWarsData);
		}

	}

}
