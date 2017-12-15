using GraphQL.Types;

namespace TestingWebApp.GraphQL {

	public class HumanInputType : InputObjectGraphType {

		public HumanInputType() {
			this.Name = "HumanInput";
			Field<NonNullGraphType<StringGraphType>>("name");
			Field<StringGraphType>("homePlanet");
		}

	}

}
