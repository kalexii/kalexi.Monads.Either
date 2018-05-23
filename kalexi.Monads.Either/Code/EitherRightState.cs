using System;
using System.Threading.Tasks;
using kalexi.Monads.Either.Exceptions;

namespace kalexi.Monads.Either.Code
{
    internal sealed class EitherRightState<TLeft, TRight> : IEither<TLeft, TRight>
    {
        public EitherRightState(TRight value) => Right = value;

        public TLeft Left => throw new EitherException("This either is Right, but client code requested Left");

        public TRight Right { get; }

        public bool IsLeft => false;

        public bool IsRight => true;

        public void Switch(Action<TLeft> leftHandAction, Action<TRight> right)
            => right(Right);

        public IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
            Func<TRight, TRight> rightHandAction)
            => new EitherRightState<TLeft, TRight>(rightHandAction(Right));

        public IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> leftHandFunction,
            Func<TRight, TRightResult> rightHandFunction)
            => new EitherRightState<TLeftResult, TRightResult>(rightHandFunction(Right));

        public async Task<IEither<TLeftResult, TRightResult>> MapAsync<TLeftResult, TRightResult>(
            Func<TLeft, Task<TLeftResult>> leftHandFunction, Func<TRight, Task<TRightResult>> rightHandFunction)
            => new EitherRightState<TLeftResult, TRightResult>(await rightHandFunction(Right));

        public void DoWithLeft(Action<TLeft> action)
        {
        }

        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default(TResult))
            => fallback;

        public void DoWithRight(Action<TRight> action)
            => action(Right);

        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default(TResult))
            => function(Right);

        public TResult Join<TResult>(Func<TLeft, TResult> leftTransform, Func<TRight, TResult> rightTransform)
            => rightTransform(Right);

        public Task<TResult> JoinAsync<TResult>(Func<TLeft, Task<TResult>> leftTransform,
            Func<TRight, Task<TResult>> rightTransform)
            => rightTransform(Right);
    }
}