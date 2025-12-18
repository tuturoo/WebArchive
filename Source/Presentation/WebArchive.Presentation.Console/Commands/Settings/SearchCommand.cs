using System.CommandLine;
using WebArchive.Core.Settings.Services;

namespace WebArchive.Presentation.Console.Commands.Settings
{
    internal sealed class SearchCommand
    {
        public static Command Create(ISettingsService settingsService)
        {
            var searchCommand = new Command("search");

            var patternArgument = new Argument<string>("pattern");
            
            searchCommand.Arguments.Add(patternArgument);

            searchCommand.SetAction((result, token) =>
            {
                var pattern = result.GetRequiredValue(patternArgument);

                System.Console.WriteLine(
                    result.GetValue(RootCommand.JsonPath));

                return SearchAsync(settingsService, pattern, token);
            });

            return searchCommand;
        }

        public static async Task SearchAsync(ISettingsService settingsService, string pattern, CancellationToken token = default)
        {
            var foundedValues = await settingsService.SearchAsync(pattern, token);
        
            if (!foundedValues.Any())
            {
                System.Console.WriteLine("По заданному паттерну не были найдены ключи");

                return;
            }

            foreach (var foundedValue in foundedValues)
            {
                System.Console.WriteLine("{0} = {1}", foundedValue.Key, foundedValue.Value);
            }
        }
    }
}
