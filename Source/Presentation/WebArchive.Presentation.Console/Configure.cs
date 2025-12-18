using Microsoft.Extensions.DependencyInjection;
using WebArchive.Application.Settings.Implementations;
using WebArchive.Infrastructure.Settings.Implementations;
using WebArchive.Core.Settings.Providers;
using WebArchive.Core.Settings.Services;

namespace WebArchive.Presentation.Console
{
    internal sealed partial class Program
    {
        public static void Configure(IServiceCollection services)
        {
            // WebArchive.Core.Settings

            services.AddSingleton<ISettingsPropertyProvider, ReflectionSettingsPropertyProvider>();
            services.AddSingleton<ISettingsProvider, MemorySettingsProvider>();
            services.AddSingleton<ISettingsService, SettingsService>();
        }
    }
}
