using System;

namespace VillaPlusTest.Domain.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Invalid request")
            : base(message) { }
    }
}