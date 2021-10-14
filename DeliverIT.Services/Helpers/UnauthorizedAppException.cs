using System;
using System.Globalization;

namespace DeliverIT.Services.Helpers
{
    public class UnauthorizedAppException : ApplicationException
    {
        public UnauthorizedAppException() : base() { }

        public UnauthorizedAppException(string message) : base(message) { }

        public UnauthorizedAppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
