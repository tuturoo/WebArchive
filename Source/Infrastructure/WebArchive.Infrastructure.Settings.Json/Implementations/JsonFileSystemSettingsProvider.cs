using System.Text.Json;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Infrastructure.Settings.Abstractions;
using WebArchive.Infrastructure.Settings.Json.Models;

namespace WebArchive.Infrastructure.Settings.Json.Implementations
{
    public sealed class JsonFileSystemSettingsProvider : FileSystemSettingsProvider
    {
        public JsonFileSystemSettingsProvider(string configPath) : base(configPath)
        {

        }

        protected override Task<ISettings> DeserializeAsync(string content, CancellationToken token = default)
        {
            return Task.FromResult(
                JsonSerializer.Deserialize<SettingsJsonModel>(content) as ISettings ?? throw new Exception("111"));
        }

        protected override Task<string> SerializeAsync(ISettings settings, CancellationToken token = default)
        {
            return Task.FromResult(
                JsonSerializer.Serialize(settings));
        }
    }
}
