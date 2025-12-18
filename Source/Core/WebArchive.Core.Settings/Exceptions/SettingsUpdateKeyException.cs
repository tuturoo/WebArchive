using WebArchive.Core.Exceptions;

namespace WebArchive.Core.Settings.Exceptions
{
	/// <summary>
	/// При обновлении ключа возникла ошибка
	/// </summary>
	[Serializable]
	public class SettingsUpdateKeyException : DomainException
	{
		public readonly string Key;

		public readonly string Value;
		
		public SettingsUpdateKeyException(string key, string value)
		{
			Key = key;
			Value = value;
		}
		
		public SettingsUpdateKeyException(string message, string key, string value) : base(message)
		{
			Key = key;
			Value = value;
		}

		public SettingsUpdateKeyException(string message, string key, string value, Exception inner) : base(message, inner)
		{
			Key = key;
			Value = value;
		}
	}
}
