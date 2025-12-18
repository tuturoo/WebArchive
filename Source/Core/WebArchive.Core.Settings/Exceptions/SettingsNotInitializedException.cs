using WebArchive.Core.Exceptions;

namespace WebArchive.Core.Settings.Exceptions
{
	/// <summary>
	/// Настройки не были инициализированы
	/// </summary>
	[Serializable]
	public class SettingsNotInitializedException : DomainException
	{
		public SettingsNotInitializedException() { }

		public SettingsNotInitializedException(string message) : base(message) { }
		
		public SettingsNotInitializedException(string message, Exception inner) : base(message, inner) { }

	}
}
