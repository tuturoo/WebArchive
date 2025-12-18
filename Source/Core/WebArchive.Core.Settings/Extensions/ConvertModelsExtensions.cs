using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Models;

namespace WebArchive.Core.Settings.Extensions
{
    /// <summary>
    /// Методы расширения для конвертации моделей
    /// </summary>
    public static class ConvertModelsExtensions
    {
        #region Settings

        /// <summary>
        /// Конвертирует внешние настройки в модель для передачи между слоями.
        /// Необходимо для избежания ситуаций, когда передаваемые настройки имеют другие свойства или их не получится сериализовать
        /// </summary>
        /// <param name="settings">Настройки для конвертации</param>
        /// <returns>Модель</returns>
        public static SettingsModel ToModel(this ISettings settings)
        {
            return new SettingsModel()
            {
                Global = settings.Global.ToModel()
            };
        }

        #endregion

        #region GlobalSettings

        /// <summary>
        /// Конвертация настроек в модель аналогично методу по конвертации SettingsModel ToModel
        /// </summary>
        /// <param name="globalSettings">Настройки для конвертации</param>
        /// <returns>Глобальные настройки</returns>
        public static GlobalSettingsModel ToModel(this IGlobalSettings globalSettings)
        {
            return new GlobalSettingsModel()
            {
                
            };
        }

        #endregion
    }
}
