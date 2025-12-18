using System;

namespace WebArchive.Core.Exceptions
{
	/// <summary>
	/// Общий тип для исключений доменного уровня
	/// </summary>
	[Serializable]
	public abstract class DomainException : Exception
	{
		public DomainException() { }
		
		public DomainException(string message) : base(message) { }

		public DomainException(string message, Exception inner) : base(message, inner) { }
	}
}
