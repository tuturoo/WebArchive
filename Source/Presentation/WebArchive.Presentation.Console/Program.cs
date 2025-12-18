using Microsoft.Extensions.DependencyInjection;
using WebArchive.Presentation.Console.Commands;
using System.CommandLine;

namespace WebArchive.Presentation.Console
{
    internal sealed partial class Program
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            Configure(services);

            return services.BuildServiceProvider();
        }

        private static async Task Main(string[] args)
        {
            await RootCommand.Build(services: CreateServiceProvider())
                .Parse(args)
                .InvokeAsync();
        }
    }
}
