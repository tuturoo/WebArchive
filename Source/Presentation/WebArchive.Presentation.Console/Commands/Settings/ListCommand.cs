using System.CommandLine;
using WebArchive.Core.Settings.Models;
using WebArchive.Core.Settings.Services;

namespace WebArchive.Presentation.Console.Commands.Settings
{
    internal sealed class ListCommand
    {
        public static Command Create(ISettingsService settingsService)
        {
            var listCommand = new Command("list");

            listCommand.SetAction((result, token) =>
                ListAsync(settingsService, token));

            return listCommand;
        }

        public static async Task ListAsync(ISettingsService settingsService, CancellationToken token = default)
        {
            var properties = await settingsService.GetPropertiesAsync(token);

            if (!properties.Any())
            {
                System.Console.WriteLine("Отсутствуют доступные ключи настроек.");

                return;
            }

            foreach (var property in properties)
            {
                System.Console.WriteLine("{0} = {1}", property.Key, property.Value);
            }
        }
    }
}
