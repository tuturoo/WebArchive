using WebArchive.Core.Settings.Models;
using WebArchive.Infrastructure.Settings.Decorators;
using WebArchive.Infrastructure.Settings.Implementations;

namespace WebArchive.Settings.Tests.Decorators
{
    public sealed class CacheSettingsProviderDecoratorTests
    {
        [Fact]
        public async Task GetAsync_WhenCacheNotIntialized_DifferentObjectsWithEqualRecord()
        {
            var settingsProvider = new MemorySettingsProvider();
            var memoryCacheDecorator = new CacheSettingsProviderDecorator(settingsProvider);

            // Устанавливаем значение в обход декоратора
            await settingsProvider.UpdateAsync(
                SettingsModel.CreateDefault());

            // Т. к. кэш не инициализирован, то он должен взять настройки из провайдера и создать новый объект на его основе, с одним содержимом

            var providerSettings = await settingsProvider.GetAsync();
            var cachedSettings = await memoryCacheDecorator.GetAsync();
        
            Assert.NotSame(cachedSettings, providerSettings);
            Assert.Equal(cachedSettings, providerSettings);

            Assert.NotSame(cachedSettings.Global, providerSettings.Global);
            Assert.Equal(cachedSettings.Global, providerSettings.Global);
        }

        [Fact]
        public async Task UpdateAsync_WhenCacheNotIntialized_DifferentObjectsWithEqualRecord()
        {
            var settingsProvider = new MemorySettingsProvider();
            var memoryCacheDecorator = new CacheSettingsProviderDecorator(settingsProvider);

            // Устанавливаем значение через декоратор, должны быть разные объекты в кэше и в сеттингс провайдере

            await memoryCacheDecorator.UpdateAsync(
                SettingsModel.CreateDefault());

            var providerSettings = await settingsProvider.GetAsync();
            var cachedSettings = await memoryCacheDecorator.GetAsync();

            Assert.NotSame(cachedSettings, providerSettings);
            Assert.Equal(cachedSettings, providerSettings);

            Assert.NotSame(cachedSettings.Global, providerSettings.Global);
            Assert.Equal(cachedSettings.Global, providerSettings.Global);
        }
    }
}
