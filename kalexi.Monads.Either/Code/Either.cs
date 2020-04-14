using System;

namespace kalexi.Monads.Either.Code
{
    /// <inheritdoc />
    public class Either<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly IEither<TLeft, TRight> eitherState;

        /// <summary>
        /// Initializes instance of <see cref="Either{TLeft,TRight}" /> with the left-hand <paramref name="value" />.
        /// </summary>
        /// <param name="value">Left-hand value to initialize this instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        public Either(TLeft value) => eitherState = new EitherLeftState<TLeft, TRight>(value);

        /// <summary>
        /// Initializes instance of <see cref="Either{TLeft,TRight}" /> with the right-hand <paramref name="value" />.
        /// </summary>
        /// <param name="value">Right-hand value to initialize this instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        public Either(TRight value) => eitherState = new EitherRightState<TLeft, TRight>(value);

        /// <inheritdoc />
        public TLeft Left => eitherState.Left;

        /// <inheritdoc />
        public TRight Right => eitherState.Right;

        /// <inheritdoc />
        public bool IsLeft => eitherState.IsLeft;

        /// <inheritdoc />
        public bool IsRight => eitherState.IsRight;

        /// <inheritdoc />
        public void Switch(Action<TLeft> leftHandAction, Action<TRight> rightHandAction)
            => eitherState.Switch(leftHandAction, rightHandAction);

        /// <inheritdoc />
        public IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
            Func<TRight, TRight> rightHandAction)
            => eitherState.NonAlteringMap(leftHandFunction, rightHandAction);

        /// <inheritdoc />
        public IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> leftHandFunction, Func<TRight, TRightResult> rightHandFunction)
            => eitherState.Map(leftHandFunction, rightHandFunction);

        /// <inheritdoc />
        public void DoWithLeft(Action<TLeft> action)
            => eitherState.DoWithLeft(action);

        /// <inheritdoc />
        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default)
            => eitherState.DoWithLeft(function, fallback);

        /// <inheritdoc />
        public void DoWithRight(Action<TRight> action)
            => eitherState.DoWithRight(action);

        /// <inheritdoc />
        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default)
            => eitherState.DoWithRight(function, fallback);

        /// <inheritdoc />
        public TResult Join<TResult>(Func<TLeft, TResult> leftTransform, Func<TRight, TResult> rightTransform)
            => eitherState.Join(leftTransform, rightTransform);

        /// <summary>
        /// Implicitly bundles left <paramref name="value"/> into <see cref="Either{TLeft,TRight}"/>.
        /// </summary>
        /// <param name="value">Value to box into an <see cref="Either{TLeft,TRight}"/></param>
        /// <returns><see cref="Either{TLeft,TRight}"/></returns>
        public static implicit operator Either<TLeft, TRight>(TLeft value)
            => new Either<TLeft, TRight>(value);

        /// <summary>
        /// Implicitly bundles right <paramref name="value"/> into <see cref="Either{TLeft,TRight}"/>.
        /// </summary>
        /// <param name="value">Value to box into an <see cref="Either{TLeft,TRight}"/></param>
        /// <returns><see cref="Either{TLeft,TRight}"/></returns>
        public static implicit operator Either<TLeft, TRight>(TRight value)
            => new Either<TLeft, TRight>(value);

        /// <summary>
        /// Creates an instance of <see cref="IEither{TLeft,TRight}" /> initialized with left-hand <paramref name="value" />.
        /// </summary>
        /// <param name="value">Left-hand value to initialize the instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        /// <returns>An instance of <see cref="IEither{TLeft,TRight}" /> initialized with left-hand <paramref name="value" /></returns>
        public static IEither<TLeft, TRight> CreateLeft(TLeft value)
            => new EitherLeftState<TLeft, TRight>(value);

        /// <summary>
        /// Creates an instance of <see cref="IEither{TLeft,TRight}" /> initialized with right-hand <paramref name="value" />.
        /// </summary>
        /// <param name="value">Right-hand value to initialize the instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        /// <returns>An instance of <see cref="IEither{TLeft,TRight}" /> initialized with right-hand <paramref name="value" /></returns>
        public static IEither<TLeft, TRight> CreateRight(TRight value)
            => new EitherRightState<TLeft, TRight>(value);
    }
}