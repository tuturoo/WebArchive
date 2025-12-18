using WebArchive.Core.Settings.Contracts;

namespace WebArchive.Core.Settings.Providers
{
    /// <summary>
    /// Провайдер настроек
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Возвращает текущие настройки
        /// </summary>
        /// <param name="token">Токен</param>
        /// <returns>Текущие настройки</returns>
        public Task<ISettings> GetAsync(CancellationToken token = default);

        /// <summary>
        /// Выполняет полное обновление настроек
        /// </summary>
        /// <param name="settings">Измененные настройки</param>
        /// <param name="token">Токен</param>
        /// <returns>Задача</returns>
        public Task UpdateAsync(ISettings settings, CancellationToken token = default);
    }
}
