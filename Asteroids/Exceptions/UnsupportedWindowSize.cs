// Жига Никита

using System;

namespace Asteroids.Exceptions
{
    public class UnsupportedWindowSize : Error
    {
        public UnsupportedWindowSize(string message) : base(message)
        {
        }

        public UnsupportedWindowSize(string message, Exception inner) : base(message, inner)
        {
        }
    }
}