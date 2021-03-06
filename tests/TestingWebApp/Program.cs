using Microsoft.AspNetCore.Hosting;

namespace TestingWebApp {

	public class Program {

		public static void Main(string[] args) =>
			BuildWebHost(args).Build().Run();

		public static IWebHostBuilder BuildWebHost(string[] args) =>
			new WebHostBuilder().UseKestrel().UseStartup<Startup>();
	}

}
