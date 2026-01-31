using System;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Providers;

namespace WebArchive.Infrastructure.Settings.Abstractions
{
    /// <summary>
    /// Базовый класс для конфига, хранящимся в файловой системе
    /// </summary>
    public abstract class FileSystemSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Путь к файлу с конфигом
        /// </summary>
        protected readonly string ConfigPath;

        protected FileSystemSettingsProvider(string configPath)
        {
            ConfigPath = configPath;
        }

        public virtual async Task<ISettings> GetAsync(CancellationToken token = default)
        {
            return await DeserializeAsync(
                content: await File.ReadAllTextAsync(ConfigPath, token),
                token: token);
        }

        public virtual async Task UpdateAsync(ISettings settings, CancellationToken token = default)
        {
            await File.WriteAllTextAsync(
                path: ConfigPath,
                contents: await SerializeAsync(settings, token),
                cancellationToken: token);
        }

        protected abstract Task<string> SerializeAsync(ISettings settings, CancellationToken token = default);

        protected abstract Task<ISettings> DeserializeAsync(string content, CancellationToken token = default);
    }
}
