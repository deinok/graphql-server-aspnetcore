using System;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Server.AspNetCore.GraphiQL.Internal;
using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore.GraphiQL {

	public class GraphiQLMiddleware : BaseMiddleware {

		private readonly GraphiQLMiddlewareSettings settings;

		public GraphiQLMiddleware(RequestDelegate nextMiddleware, GraphiQLMiddlewareSettings settings) : base(nextMiddleware) {
			this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		public async override Task Invoke(HttpContext httpContext) {
			if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

			if (this.IsGraphiQLRequest(httpContext.Request)) {
				await this.InvokeGraphiQL(httpContext.Response).ConfigureAwait(false);
				return;
			}

			await this.NextMiddleware(httpContext).ConfigureAwait(false);
		}

		private bool IsGraphiQLRequest(HttpRequest httpRequest) {
			return httpRequest.Path.StartsWithSegments(this.settings.GraphiQLPath)
				&& string.Equals(httpRequest.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase);
		}

		private async Task InvokeGraphiQL(HttpResponse httpResponse) {
			httpResponse.ContentType = "text/html";
			httpResponse.StatusCode = 200;

			// TODO: use RazorPageGenerator when ASP.NET Core 1.1 is out...?
			var builder = new StringBuilder(GraphiQLHTMLSettings.HTML);
			builder.Replace("@Model.GraphQLPath", this.settings.GraphQLPath);

			var data = Encoding.UTF8.GetBytes(builder.ToString());
			await httpResponse.Body.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
		}

	}

}
