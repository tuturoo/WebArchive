using System.CommandLine;
using WebArchive.Core.Settings.Exceptions;
using WebArchive.Core.Settings.Services;

namespace WebArchive.Presentation.Console.Commands.Settings
{
    internal sealed class UpdateCommand
    {
        public static Command Create(ISettingsService settingsService)
        {
            var updateCommand = new Command("update");

            var keyArgument = new Argument<string>("key");
            var valueArgument = new Argument<string>("value");

            updateCommand.Arguments.Add(keyArgument);
            updateCommand.Arguments.Add(valueArgument);

            updateCommand.SetAction((result, token) =>
            {
                return UpdateAsync(
                    settingsService: settingsService,
                    key: result.GetRequiredValue(keyArgument),
                    value: result.GetRequiredValue(valueArgument),
                    token: token);
            });
            
            return updateCommand;
        }

        public static async Task UpdateAsync(ISettingsService settingsService, string key, string value, CancellationToken token = default)
        {
            try
            {
                await settingsService.UpdatePropertyAsync(key, value, token);

                System.Console.WriteLine("Успешно обновлено.");
            }
            catch (SettingsKeyNotFoundException)
            {
                System.Console.WriteLine("Свойство по заданному ключу не было найдено.");
            }
            catch (SettingsUpdateKeyException ex)
            {
                System.Console.WriteLine("Произошла ошибка при попытке изменить значение: {0}", ex.InnerException?.Message ?? "неизвестная ошибка");
            }
            catch (Exception ex) 
            {
                System.Console.WriteLine("Произошла ошибка: {0}", ex.Message);
            }
        }
    }
}
