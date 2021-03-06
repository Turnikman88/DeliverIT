using System;
using System.Globalization;

namespace DeliverIT.Services.Helpers
{
    public class AppException : ApplicationException
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
