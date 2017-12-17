using System.IO;
using System.Reflection;

namespace GraphQL.Server.AspNetCore.GraphiQL.Internal {

	internal class GraphiQLHTMLSettings {

		private static string graphiQLCSHtml;

		public static string HTML {
			get {
				if (graphiQLCSHtml != null) {
					return graphiQLCSHtml;
				}
				var assembly = typeof(GraphiQLHTMLSettings).GetTypeInfo().Assembly;
				var resource = assembly.GetManifestResourceStream("GraphQL.Server.AspNetCore.GraphiQL.Internal.graphiql.cshtml");
				graphiQLCSHtml= new StreamReader(resource).ReadToEnd();
				return HTML;
			}
			set {
				graphiQLCSHtml = value;
			}
		}

	}

}
