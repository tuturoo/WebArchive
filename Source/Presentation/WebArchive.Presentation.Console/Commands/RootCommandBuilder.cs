using System.CommandLine;
using WebArchive.Presentation.Console.Commands.Settings;

namespace WebArchive.Presentation.Console.Commands
{
    internal static class RootCommandBuilder
    {
        extension (RootCommand)
        {
            public static Option<string> JsonPath => new Option<string>("--json")
            {
                DefaultValueFactory = argument => "./",
                Required = true,
            };

            public static RootCommand Build(IServiceProvider services)
            {
                var command = new RootCommand("Работа с локальным веб-архивом")
                {
                    SettingsCommand.Create(services)
                };

                command.Options.Add(RootCommand.JsonPath);

                return command;
            }
        }
    }
}
