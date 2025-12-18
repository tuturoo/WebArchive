using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using WebArchive.Core.Settings.Models;
using WebArchive.Core.Settings.Services;
using WebArchive.Presentation.Console.Commands;

namespace WebArchive.Presentation.Console.Commands.Settings
{
    internal sealed class SettingsCommand
    {
        public static Command Create(IServiceProvider services)
        {
            var settingsService = services.GetRequiredService<ISettingsService>();

            var settingsCommand = new Command("settings")
            {
                SearchCommand.Create(settingsService),
                ListCommand.Create(settingsService),
                UpdateCommand.Create(settingsService),
            };

            return settingsCommand;
        }
    }
}
