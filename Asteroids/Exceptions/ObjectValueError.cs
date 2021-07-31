using System;

namespace Asteroids.Exceptions
{
    public class ObjectValueError : Error
    {
        public ObjectValueError(string message) : base(message)
        {
        }

        public ObjectValueError(string message, Exception inner) : base(message, inner)
        {
        }
    }
}