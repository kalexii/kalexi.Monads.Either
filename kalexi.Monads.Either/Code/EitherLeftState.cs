using System;
using kalexi.Monads.Either.Exceptions;

namespace kalexi.Monads.Either.Code
{
    internal sealed class EitherLeftState<TLeft, TRight> : IEither<TLeft, TRight>
    {
        public EitherLeftState(TLeft value) => Left = value;

        public TLeft Left { get; }

        public TRight Right => throw new EitherException("This either is Left, but client code requested Right");

        public bool IsLeft => true;

        public bool IsRight => false;

        public void Switch(Action<TLeft> leftHandAction, Action<TRight> rightHandAction)
            => leftHandAction(Left);

        public IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
            Func<TRight, TRight> rightHandAction)
            => new EitherLeftState<TLeft, TRight>(leftHandFunction(Left));

        public IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> leftHandFunction,
            Func<TRight, TRightResult> rightHandFunction)
            => new EitherLeftState<TLeftResult, TRightResult>(leftHandFunction(Left));

        public void DoWithLeft(Action<TLeft> action)
            => action(Left);

        public TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default)
            => function(Left);

        public void DoWithRight(Action<TRight> action)
        {
        }

        public TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default)
            => fallback;

        public TResult Join<TResult>(Func<TLeft, TResult> leftTransform, Func<TRight, TResult> rightTransform)
            => leftTransform(Left);
    }
}