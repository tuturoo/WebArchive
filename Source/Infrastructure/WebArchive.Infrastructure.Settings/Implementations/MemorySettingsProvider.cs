using Nito.AsyncEx;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Exceptions;
using WebArchive.Core.Settings.Providers;

namespace WebArchive.Infrastructure.Settings.Implementations
{
    /// <summary>
    /// Используем в качестве хранилища оперативную память.
    /// Класс только для тестирования, т.к. не обеспечивает постоянное хранение
    /// </summary>
    public sealed class MemorySettingsProvider : ISettingsProvider
    {
        private readonly AsyncReaderWriterLock _lock;
        
        private ISettings? _currentSettings;

        public MemorySettingsProvider()
        {
            _lock = new AsyncReaderWriterLock();
        }

        /// <inheritdoc />
        public async Task<ISettings> GetAsync(CancellationToken token = default)
        {
            if (_currentSettings is null)
            {
                const string errorMessage = $"{nameof(MemorySettingsProvider)} требует первичной инициализации через UpdateAsync";

                throw new SettingsNotInitializedException(errorMessage);
            }

            return _currentSettings;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(ISettings settings, CancellationToken token = default)
        {
            _currentSettings = settings;
        }
    }
}
