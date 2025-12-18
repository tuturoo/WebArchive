using WebArchive.Core.Settings.Providers;
using WebArchive.Core.Settings.Contracts;

namespace WebArchive.Core.Settings.Services
{
    /// <summary>
    /// Сервис для работы с настройками
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Возвращает текущие настройки
        /// </summary>
        /// <param name="token">Токен</param>
        /// <returns>Текущие настройки</returns>
        public Task<ISettings> GetAsync(CancellationToken token = default);

        /// <summary>
        /// Выполняет поиск свойства по вхождению подстроки
        /// </summary>
        /// <param name="pattern">Подстрока, которая должна присутствовать в ключе</param>
        /// <param name="token">Токен</param>
        /// <returns>Пары ключ - значение, где ключ имеет вхождение подстроки</returns>
        public Task<IEnumerable<KeyValuePair<string, string>>> SearchAsync(string pattern, CancellationToken token = default);

        /// <summary>
        /// Метод для получения настроек приложения в виде пары ключ - значение.
        /// Вид этих пар определяется <see cref="ISettingsPropertyProvider"/>
        /// </summary>
        /// <param name="token">Токен</param>
        /// <returns>Пары ключ - значения</returns>
        public Task<IEnumerable<KeyValuePair<string, string>>> GetPropertiesAsync(CancellationToken token = default);

        /// <summary>
        /// Обновление определенного свойства по ключу
        /// Вид ключа определяется <see cref="ISettingsPropertyProvider"/>
        /// </summary>
        /// <param name="key">Здесь ключ - это путь к свойству</param>
        /// <param name="value">Значение свойства</param>
        /// <param name="token">Токен</param>
        /// <returns>Задача</returns>
        public Task UpdatePropertyAsync(string key, string value, CancellationToken token = default);

        /// <summary>
        /// Полное обновление настроек
        /// </summary>
        /// <param name="settings">Настройки для обновления</param>
        /// <param name="token">Токен</param>
        /// <returns>Задача</returns>
        public Task UpdateAsync(ISettings settings, CancellationToken token = default);
    }
}
