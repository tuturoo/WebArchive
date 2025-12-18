using Nito.AsyncEx;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Providers;

namespace WebArchive.Infrastructure.Settings.Decorators
{
    /// <summary>
    /// Декоратор для безопасного изменения/чтения настроек
    /// </summary>
    public sealed class ThreadSafeSettingsProviderDecorator : ISettingsProvider
    {
        private readonly AsyncReaderWriterLock _lock;
        private readonly ISettingsProvider _inner;

        public ThreadSafeSettingsProviderDecorator(ISettingsProvider inner)
        {
            _lock = new AsyncReaderWriterLock();
            _inner = inner;
        }

        // <inheritdoc/>
        public async Task<ISettings> GetAsync(CancellationToken token = default)
        {
            using (await _lock.ReaderLockAsync(token))
            {
                return await _inner.GetAsync(token);
            }
        }

        // <inheritdoc/>
        public async Task UpdateAsync(ISettings settings, CancellationToken token = default)
        {
            using (await _lock.WriterLockAsync(token))
            {
                await _inner.UpdateAsync(settings, token);
            }
        }
    }
}
