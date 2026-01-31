using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Extensions;
using WebArchive.Core.Settings.Providers;

namespace WebArchive.Infrastructure.Settings.Decorators
{
    /// <summary>
    /// Декоратор для провайдера настроек с использованием в качестве кэша оперативную память.
    /// При первичной инициализации использует семафор
    /// </summary>
    public sealed class CacheSettingsProviderDecorator : ISettingsProvider
    {
        private readonly ISettingsProvider _innerProvider;
        private readonly SemaphoreSlim _semaphore;

        private ISettings? _currentSettings;

        public CacheSettingsProviderDecorator(ISettingsProvider settingsProvider)
        {
            _semaphore = new SemaphoreSlim(1);
            _innerProvider = settingsProvider;
        }

        // <inheritdoc/>
        public async Task<ISettings> GetAsync(CancellationToken token = default)
        {
            if (_currentSettings is not null)
                return _currentSettings;

            await _semaphore.WaitAsync(token);
            try
            {
                _currentSettings ??= await _innerProvider.GetAsync(token);

                return _currentSettings;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // <inheritdoc/>
        public async Task UpdateAsync(ISettings settings, CancellationToken token = default)
        {
            _currentSettings = settings.ToModel();

            await _innerProvider.UpdateAsync(settings, token);
        }
    }
}
