
namespace WebArchive.Core.Settings.Contracts
{
    /// <summary>
    /// Интерфейс, которому должны соответствовать все настройки
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Глобальные настройки приложения
        /// </summary>
        public IGlobalSettings Global { get; }
    }
}
