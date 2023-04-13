using System;
namespace VillaApi.Logging
{
	public interface ILogging
	{
		public void log(string message, string type);
	}
}

