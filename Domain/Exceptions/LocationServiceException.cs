using System;

namespace Domain.Exceptions
{
    public class LocationServiceException : Exception
    {
        public LocationServiceException(string message) : base(message)
        {
        }
    }
}
