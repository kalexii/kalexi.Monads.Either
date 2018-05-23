using System;

namespace kalexi.Monads.Either.Exceptions
{
    public class EitherException : Exception
    {
        public EitherException(string message) : base(message)
        {
        }
    }
}
