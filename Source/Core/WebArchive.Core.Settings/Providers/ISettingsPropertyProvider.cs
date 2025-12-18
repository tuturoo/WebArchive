using WebArchive.Core.Settings.Contracts;

namespace WebArchive.Core.Settings.Providers
{
    /// <summary>
    /// Интерфейс по работе со свойствами настроек
    /// </summary>
    public interface ISettingsPropertyProvider
    {
        /// <summary>
        /// Возвращает пары ключ - значение переданных настроек
        /// </summary>
        /// <param name="settings">Настройки</param>
        /// <param name="token">Токен</param>
        /// <returns>Пары ключ - значения</returns>
        public Task<IEnumerable<KeyValuePair<string, string>>> GetPropertiesListAsync(ISettings settings, CancellationToken token = default);

        /// <summary>
        /// Обновляет настройки по указанному ключу
        /// </summary>
        /// <param name="settings">Настройки для обновления</param>
        /// <param name="key">Ключ свойства</param>
        /// <param name="value">Значение для обновления</param>
        /// <param name="token">Токен</param>
        /// <returns>Задача</returns>
        public Task UpdatePropertyAsync(ISettings settings, string key, string value, CancellationToken token = default);
    }
}
