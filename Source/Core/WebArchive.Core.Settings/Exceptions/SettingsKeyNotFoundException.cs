using WebArchive.Core.Exceptions;

namespace WebArchive.Core.Settings.Exceptions
{
    /// <summary>
    /// При выполнении операции, подразумевающей изменение существующего ключа настроек - ключ не был найден
    /// </summary>
    [Serializable]
    public class SettingsKeyNotFoundException : DomainException
    {
        public readonly string NotFoundKey;
        
        public SettingsKeyNotFoundException(string message, string notFoundKey) : base(message)
        {
            NotFoundKey = notFoundKey;
        }
        
        public SettingsKeyNotFoundException(string message, string notFoundKey, Exception inner) : base(message, inner)
        {
            NotFoundKey = notFoundKey;
        }
    }
}
