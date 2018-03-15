using System;
using kalexi.Monads.Either.Exceptions;

namespace kalexi.Monads.Either.Code
{
    public class Either<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly IEither<TLeft, TRight> actualEither;

        /// <summary>
        /// Initializes instance of <see cref="Either{TLeft,TRight}" /> with the left-hand <see cref="value" />.
        /// </summary>
        /// <param name="value">Left-hand value to initialize this instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        public Either(TLeft value) => actualEither = new EitherLeft<TLeft, TRight>(value);

        /// <summary>
        /// Initializes instance of <see cref="Either{TLeft,TRight}" /> with the right-hand <see cref="value" />.
        /// </summary>
        /// <param name="value">Right-hand value to initialize this instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        public Either(TRight value) => actualEither = new EitherRight<TLeft, TRight>(value);

        /// <inheritdoc />
        public TLeft Left => actualEither.Left;

        /// <inheritdoc />
        public TRight Right => actualEither.Right;

        /// <inheritdoc />
        public bool IsLeft => actualEither.IsLeft;

        /// <inheritdoc />
        public bool IsRight => actualEither.IsRight;

        /// <inheritdoc />
        public void Switch(Action<TLeft> leftHandAction, Action<TRight> rightHandAction)
            => actualEither.Switch(leftHandAction, rightHandAction);

        /// <inheritdoc />
        public IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
                                                     Func<TRight, TRight> rightHandAction)
            => actualEither.NonAlteringMap(leftHandFunction, rightHandAction);

        /// <inheritdoc />
        public IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> leftHandFunction, Func<TRight, TRightResult> rightHandFunction)
            => actualEither.Map(leftHandFunction, rightHandFunction);

        /// <inheritdoc />
        public void DoWithLeft(Action<TLeft> action)
            => actualEither.DoWithLeft(action);

        /// <inheritdoc />
        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default(TResult))
            => actualEither.DoWithLeft(function, fallback);

        /// <inheritdoc />
        public void DoWithRight(Action<TRight> action)
            => actualEither.DoWithRight(action);

        /// <inheritdoc />
        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default(TResult))
            => actualEither.DoWithRight(function, fallback);

        public static implicit operator Either<TLeft, TRight>(TLeft value)
            => new Either<TLeft, TRight>(value);

        public static implicit operator Either<TLeft, TRight>(TRight value)
            => new Either<TLeft, TRight>(value);

        /// <summary>
        /// Creates an instance of <see cref="IEither{TLeft,TRight}" /> initialized with left-hand <see cref="value" />.
        /// </summary>
        /// <param name="value">Left-hand value to initialize the instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        /// <returns>An instance of <see cref="IEither{TLeft,TRight}" /> initialized with left-hand <see cref="value" /></returns>
        public static IEither<TLeft, TRight> CreateLeft(TLeft value)
            => new EitherLeft<TLeft, TRight>(value);

        /// <summary>
        /// Creates an instance of <see cref="IEither{TLeft,TRight}" /> initialized with right-hand <see cref="value" />.
        /// </summary>
        /// <param name="value">Right-hand value to initialize the instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        /// <returns>An instance of <see cref="IEither{TLeft,TRight}" /> initialized with right-hand <see cref="value" /></returns>
        public static IEither<TLeft, TRight> CreateRight(TRight value)
            => new EitherRight<TLeft, TRight>(value);
    }

    internal struct EitherLeft<TLeft, TRight> : IEither<TLeft, TRight>
    {
        public EitherLeft(TLeft value) => Left = value;

        public TLeft Left { get; }

        public TRight Right => throw new EitherException("This either is Left, but client code requested Right");

        public bool IsLeft => true;

        public bool IsRight => false;

        public void Switch(Action<TLeft> leftHandAction, Action<TRight> rightHandAction)
            => leftHandAction(Left);

        public IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
                                                     Func<TRight, TRight> rightHandAction)
            => new EitherLeft<TLeft, TRight>(leftHandFunction(Left));

        public IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> leftHandFunction,
            Func<TRight, TRightResult> rightHandFunction)
            => new EitherLeft<TLeftResult, TRightResult>(leftHandFunction(Left));

        public void DoWithLeft(Action<TLeft> action)
            => action(Left);

        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default(TResult))
            => function(Left);

        public void DoWithRight(Action<TRight> action)
        {
        }

        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default(TResult))
            => fallback;
    }

    internal struct EitherRight<TLeft, TRight> : IEither<TLeft, TRight>
    {
        public EitherRight(TRight value) => Right = value;

        public TLeft Left => throw new EitherException("This either is Right, but client code requested Left");

        public TRight Right { get; }

        public bool IsLeft => false;

        public bool IsRight => true;

        public void Switch(Action<TLeft> leftHandAction, Action<TRight> right)
            => right(Right);

        public IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
                                                     Func<TRight, TRight> rightHandAction)
            => new EitherRight<TLeft, TRight>(rightHandAction(Right));

        public IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> leftHandFunction,
            Func<TRight, TRightResult> rightHandFunction)
            => new EitherRight<TLeftResult, TRightResult>(rightHandFunction(Right));

        public void DoWithLeft(Action<TLeft> action)
        {
        }

        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default(TResult))
            => fallback;

        public void DoWithRight(Action<TRight> action)
            => action(Right);

        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default(TResult))
            => function(Right);
    }
}
