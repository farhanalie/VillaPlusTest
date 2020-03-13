using System;

namespace VillaPlus.API.Domain.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Invalid request")
            : base(message) { }
    }
}