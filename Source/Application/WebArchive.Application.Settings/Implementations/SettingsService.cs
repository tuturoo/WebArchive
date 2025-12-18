using WebArchive.Core.Settings.Extensions;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Providers;
using WebArchive.Core.Settings.Services;
using WebArchive.Core.Settings.Exceptions;
using WebArchive.Core.Settings.Models;

namespace WebArchive.Application.Settings.Implementations
{
    // <inheritdoc/>
    public sealed class SettingsService : ISettingsService
    {
        private readonly ISettingsPropertyProvider _propertyProvider; 
        private readonly ISettingsProvider _settingsProvider;

        public SettingsService(ISettingsPropertyProvider propertyProvider, ISettingsProvider provider)
        {
            _propertyProvider = propertyProvider;
            _settingsProvider = provider;
        }

        // <inheritdoc/>
        public async Task<IEnumerable<KeyValuePair<string, string>>> SearchAsync(string pattern, CancellationToken token = default)
        {
            var properties = await GetPropertiesAsync(token);

            return properties.Where(p => p.Key.Contains(pattern));
        }

        /// <inheritdoc/>
        public async Task<ISettings> GetAsync(CancellationToken token = default)
        {
            try
            {
                var settings = await _settingsProvider.GetAsync(token);

                // При получении настроек из провайдера всегда кастим к модели

                return settings.ToModel();
            }
            catch (SettingsNotInitializedException)
            {
                var defaultSettings = SettingsModel.CreateDefault();

                await _settingsProvider.UpdateAsync(defaultSettings, token);

                return defaultSettings;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPropertiesAsync(CancellationToken token = default)
        {
            ISettings currentSettings;

            try
            {
                currentSettings = await _settingsProvider.GetAsync(token);
            }
            catch (SettingsNotInitializedException)
            {
                currentSettings = SettingsModel.CreateDefault();

                await _settingsProvider.UpdateAsync(currentSettings, token);
            }

            var currentSettingsModel = currentSettings.ToModel();

            // При получении настроек из провайдера всегда кастим к модели

            var properties = await _propertyProvider.GetPropertiesListAsync(currentSettingsModel, token);

            return properties.OrderByDescending(pair => pair.Key.Length);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(ISettings settings, CancellationToken token = default)
        {
            await _settingsProvider.UpdateAsync(settings, token);
        }

        /// <inheritdoc/>
        public async Task UpdatePropertyAsync(string key, string value, CancellationToken token = default)
        {
            ISettings currentSettings;

            try
            {
                currentSettings = await _settingsProvider.GetAsync(token);
            }
            catch (SettingsNotInitializedException)
            {
                currentSettings = SettingsModel.CreateDefault();

                await _settingsProvider.UpdateAsync(currentSettings, token);
            }

            var currentSettingsModel = currentSettings.ToModel();

            await _propertyProvider.UpdatePropertyAsync(currentSettingsModel, key, value, token);

            await _settingsProvider.UpdateAsync(currentSettingsModel, token);
        }
    }
}
