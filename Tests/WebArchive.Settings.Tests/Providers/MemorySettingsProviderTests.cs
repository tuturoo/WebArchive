using WebArchive.Infrastructure.Settings.Implementations;
using WebArchive.Core.Settings.Exceptions;
using Moq;
using WebArchive.Core.Settings.Contracts;

namespace WebArchive.Settings.Tests.Providers
{
    public sealed class MemorySettingsProviderTests
    {
        [Fact]
        public async Task GetAsync_WhenNotInitialized_ThrowsSettingsNotInitialized()
        {
            var settingsProvider = new MemorySettingsProvider();

            var exception = await Assert.ThrowsAsync<SettingsNotInitializedException>(async () =>
                await settingsProvider.GetAsync());

            Assert.Equal(
                $"{nameof(MemorySettingsProvider)} требует первичной инициализации через UpdateAsync",
                exception.Message);
        }

        [Fact]
        public async Task GetAsync_AfterUpdating_ObjectReferenceEquals()
        {
            var settingsProvider = new MemorySettingsProvider();
            var settingsToUpdate = new Mock<ISettings>().Object;

            await settingsProvider.UpdateAsync(settingsToUpdate);

            var retrievedSettings = await settingsProvider.GetAsync();

            Assert.Same(settingsToUpdate, retrievedSettings);
            Assert.Equal(settingsToUpdate, retrievedSettings);
        }
    }
}
