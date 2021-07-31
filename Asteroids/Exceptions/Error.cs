// Жига Никита

using System;

namespace Asteroids.Exceptions
{
    public class Error : Exception
    {
        public Error(string message) : base(message)
        {
        }

        public Error(string message, Exception inner) : base(message, inner)
        {
        }
    }
}