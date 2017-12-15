using System;
using GraphQL.Types;
using TestingWebApp.GraphQL.Types;

namespace TestingWebApp.GraphQL {

	public class StarWarsQuery : ObjectGraphType<object> {

		public StarWarsQuery(StarWarsData data) {
			this.Name = "Query";

			Field<CharacterInterface>("hero", resolve: context => data.GetDroidByIdAsync("3"));
			Field<HumanType>(
				"human",
				arguments: new QueryArguments(
					new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
				),
				resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
			);

			Func<ResolveFieldContext<object>, string, object> func = (context, id) => data.GetDroidByIdAsync(id);

			FieldDelegate<DroidType>(
				"droid",
				arguments: new QueryArguments(
					new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
				),
				resolve: func
			);
		}

	}

}
