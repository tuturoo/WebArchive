using WebArchive.Core.Settings.Contracts;

namespace WebArchive.Core.Settings.Models
{
    /// <summary>
    /// Модель настроек приложения
    /// </summary>
    public sealed record SettingsModel : ISettings
    {
        /// <summary>
        /// Создает тестовый конфиг
        /// </summary>
        public static SettingsModel CreateDefault()
        {
            return new SettingsModel();
        }

        /// <summary>
        /// Глобальные настройки
        /// </summary>
        public GlobalSettingsModel Global { get; set; } = new GlobalSettingsModel();

        /// <summary>
        /// Глобальные настройки - явная реализация интерфейса, чтобы мы обращались к типизированной модели, которой можно изменять значения
        /// </summary>
        IGlobalSettings ISettings.Global => Global;
    }
}
