using System;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Providers;

namespace WebArchive.Infrastructure.Settings.Abstractions
{
    public abstract class FileSystemSettingsProvider : ISettingsProvider
    {
        private readonly string _path;

        protected FileSystemSettingsProvider(string path)
        {
            _path = path;
        }

        public Task<ISettings> GetAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ISettings settings, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        protected abstract Task<string> SerializeAsync(ISettings settings, CancellationToken token = default);

        protected abstract Task<ISettings> DeserializeAsync(string content, CancellationToken token = default);
    }
}
