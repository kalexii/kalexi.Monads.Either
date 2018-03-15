using System;

namespace kalexi.Monads.Either.Exceptions
{
    public class EitherException : Exception
    {
        public EitherException()
        {
        }

        public EitherException(string message) : base(message)
        {
        }
    }
}
