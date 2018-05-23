using System;
using System.Threading.Tasks;

namespace kalexi.Monads.Either.Code
{
    /// <inheritdoc />
    public class Either<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly IEither<TLeft, TRight> eitherState;

        /// <summary>
        /// Initializes instance of <see cref="Either{TLeft,TRight}" /> with the left-hand <see cref="value" />.
        /// </summary>
        /// <param name="value">Left-hand value to initialize this instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        public Either(TLeft value) => eitherState = new EitherLeftState<TLeft, TRight>(value);

        /// <summary>
        /// Initializes instance of <see cref="Either{TLeft,TRight}" /> with the right-hand <see cref="value" />.
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
        public Task<IEither<TLeftResult, TRightResult>> MapAsync<TLeftResult, TRightResult>(
            Func<TLeft, Task<TLeftResult>> leftHandFunction, Func<TRight, Task<TRightResult>> rightHandFunction)
            => eitherState.MapAsync(leftHandFunction, rightHandFunction);

        /// <inheritdoc />
        public void DoWithLeft(Action<TLeft> action)
            => eitherState.DoWithLeft(action);

        /// <inheritdoc />
        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default(TResult))
            => eitherState.DoWithLeft(function, fallback);

        /// <inheritdoc />
        public void DoWithRight(Action<TRight> action)
            => eitherState.DoWithRight(action);

        /// <inheritdoc />
        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default(TResult))
            => eitherState.DoWithRight(function, fallback);

        /// <inheritdoc />
        public TResult Join<TResult>(Func<TLeft, TResult> leftTransform, Func<TRight, TResult> rightTransform)
            => eitherState.Join(leftTransform, rightTransform);

        /// <inheritdoc />
        public Task<TResult> JoinAsync<TResult>(Func<TLeft, Task<TResult>> leftTransform,
            Func<TRight, Task<TResult>> rightTransform)
            => eitherState.JoinAsync(leftTransform, rightTransform);

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
            => new EitherLeftState<TLeft, TRight>(value);

        /// <summary>
        /// Creates an instance of <see cref="IEither{TLeft,TRight}" /> initialized with right-hand <see cref="value" />.
        /// </summary>
        /// <param name="value">Right-hand value to initialize the instance of <see cref="IEither{TLeft,TRight}" /> with.</param>
        /// <returns>An instance of <see cref="IEither{TLeft,TRight}" /> initialized with right-hand <see cref="value" /></returns>
        public static IEither<TLeft, TRight> CreateRight(TRight value)
            => new EitherRightState<TLeft, TRight>(value);
    }
}