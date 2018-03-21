using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace GraphQL.Server.AspNetCore.Internal {

	internal static class GraphQLServiceExtensions {

		public static Inputs ToInputs(this JToken obj) {
			if (obj == null) { throw new ArgumentNullException(nameof(obj)); }

			var variables = obj.GetValue() as Dictionary<string, object> ?? new Dictionary<string, object>();
			return new Inputs(variables);
		}

	}

}
