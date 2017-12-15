using GraphQL.Types;

namespace GraphQL.StarWars {

	public class HumanInputType : InputObjectGraphType {

		public HumanInputType() {
			this.Name = "HumanInput";
			Field<NonNullGraphType<StringGraphType>>("name");
			Field<StringGraphType>("homePlanet");
		}

	}

}
