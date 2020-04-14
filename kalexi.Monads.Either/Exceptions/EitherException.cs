using System;

namespace kalexi.Monads.Either.Exceptions
{
    /// <summary>
    /// Thrown if there is an Either-related problem.
    /// </summary>
    public class EitherException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="EitherException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public EitherException(string message) : base(message)
        {
        }
    }
}