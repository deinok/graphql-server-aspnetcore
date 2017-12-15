using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GraphQL.Server.AspNetCore {

	public interface IMiddleware {

		Task Invoke(HttpContext httpContext);

	}

	public abstract class BaseMiddleware : IMiddleware {

		protected RequestDelegate NextMiddleware { get; }

		public BaseMiddleware(RequestDelegate nextMiddleware) {
			this.NextMiddleware = nextMiddleware ?? throw new ArgumentNullException(nameof(nextMiddleware));
		}

		public abstract Task Invoke(HttpContext httpContext);

	}

}
