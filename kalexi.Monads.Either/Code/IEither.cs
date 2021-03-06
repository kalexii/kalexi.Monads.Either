﻿using System;
using System.Diagnostics.CodeAnalysis;
using kalexi.Monads.Either.Exceptions;

namespace kalexi.Monads.Either.Code
{
    /// <summary>
    /// Either monad encapsulates the outcome that can be of two different types. Either <typeparamref name="TLeft"/> or <typeparamref name="TRight"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotThrown")]
    public interface IEither<TLeft, TRight>
    {
        /// <summary>
        /// Retrieves left-hand value from current instance of <see cref="IEither{TLeft,TRight}" />, and throws if current instance
        /// of <see cref="IEither{TLeft,TRight}" />.<see cref="IsRight" />.
        /// </summary>
        /// <exception cref="EitherException">This either is Left, but client code requested Right</exception>
        TLeft Left { get; }

        /// <summary>
        /// Retrieves right-hand value from current instance of <see cref="IEither{TLeft,TRight}" />, and throws if current
        /// instance of <see cref="IEither{TLeft,TRight}" />.<see cref="IsLeft" />.
        /// </summary>
        /// <exception cref="EitherException">This either is Left, but client code requested Right</exception>
        TRight Right { get; }

        /// <summary>
        /// Indicates whether current instance of <see cref="IEither{TLeft,TRight}" /> has left-hand value populated, and not the
        /// right-hand one.
        /// </summary>
        bool IsLeft { get; }

        /// <summary>
        /// Indicates whether current instance of <see cref="IEither{TLeft,TRight}" /> has it's right-hand value populated, and not
        /// the left-hand one.
        /// </summary>
        bool IsRight { get; }

        /// <summary>
        /// Executes one of provided actions depending on which value is held in this instance of
        /// <see cref="IEither{TLeft,TRight}" />.
        /// </summary>
        /// <param name="leftHandAction">
        /// Action to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        /// <param name="rightHandAction">
        /// Action to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        void Switch(Action<TLeft> leftHandAction, Action<TRight> rightHandAction);

        /// <summary>
        /// Executes one of provided functions depending on which value is held in this instance of
        /// <see cref="IEither{TLeft,TRight}" /> and returns <see cref="IEither{TLeft,TRight}" /> as result.
        /// This overload exists to allow the calling code to omit specifying result type arguments if desired result types are
        /// <typeparamref name="TLeft"/> and <typeparamref name="TRight"/>.
        /// </summary>
        /// <param name="leftHandFunction">
        /// Function to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        /// <param name="rightHandAction">
        /// Function to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        /// <returns>
        /// Instance of <see cref="IEither{TLeft,TRight}" /> populated with result value of function chosen by the value
        /// of the current instance of <see cref="IEither{TLeft,TRight}" />.
        /// </returns>
        IEither<TLeft, TRight> NonAlteringMap(Func<TLeft, TLeft> leftHandFunction,
            Func<TRight, TRight> rightHandAction);

        /// <summary>
        /// Executes one of provided functions depending on which value is held in this instance of
        /// <see cref="IEither{TLeft,TRight}" /> and returns <see cref="IEither{TLeftResult,TRightResult}" /> as result.
        /// </summary>
        /// <param name="leftHandFunction">
        /// Function to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        /// <param name="rightHandFunction">
        /// Function to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        /// <returns>
        /// Instance of <see cref="IEither{TLeftResult,TRightResult}" /> populated with result value of function chosen by
        /// the value of the current instance of <see cref="IEither{TLeft,TRight}" />.
        /// </returns>
        IEither<TLeftResult, TRightResult> Map<TLeftResult, TRightResult>(Func<TLeft, TLeftResult> leftHandFunction,
            Func<TRight, TRightResult> rightHandFunction);


        /// <summary>
        /// If current instance of <see cref="IEither{TLeft,TRight}" />.<see cref="IsLeft" />,
        /// executes <paramref name="action" /> passing <see cref="IEither{TLeft,TRight}" />.<see cref="Left" /> as argument.
        /// Otherwise, does nothing.
        /// </summary>
        /// <param name="action">
        /// Action to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        void DoWithLeft(Action<TLeft> action);

        /// <summary>
        /// If current instance of <see cref="IEither{TLeft,TRight}" />.<see cref="IsLeft" />,
        /// executes <paramref name="function" /> passing <see cref="IEither{TLeft,TRight}" />.<see cref="Left" /> as argument and
        /// returns the result value of type <typeparamref name="TResult" />.
        /// Otherwise, returns <paramref name="fallback"/> value.
        /// </summary>
        /// <param name="function">
        /// Function to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        /// <param name="fallback">
        /// Value to return in case if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        TResult DoWithLeft<TResult>(Func<TLeft, TResult> function, TResult fallback = default);

        /// <summary>
        /// If current instance of <see cref="IEither{TLeft,TRight}" />.<see cref="IsRight" />,
        /// executes <paramref name="action" /> passing <see cref="IEither{TLeft,TRight}" />.<see cref="Right" /> as argument.
        /// Otherwise, does nothing.
        /// </summary>
        /// <param name="action">
        /// Action to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        void DoWithRight(Action<TRight> action);

        /// <summary>
        /// If current instance of <see cref="IEither{TLeft,TRight}" />.<see cref="IsRight" />,
        /// executes <paramref name="function" /> passing <see cref="IEither{TLeft,TRight}" />.<see cref="Right" /> as argument and
        /// returns the result value of type <typeparamref name="TResult" />.
        /// Otherwise, returns <paramref name="fallback" /> value.
        /// </summary>
        /// <param name="function">
        /// Function to execute if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        /// <param name="fallback">
        /// Value to return in case if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        TResult DoWithRight<TResult>(Func<TRight, TResult> function, TResult fallback = default);

        /// <summary>
        /// Transforms either <see cref="Left" /> or <see cref="Right" /> using <paramref name="leftTransform" /> or
        /// <paramref name="rightTransform" /> correspondingly to produce value of a single type.
        /// </summary>
        /// <typeparam name="TResult">Type of produced value.</typeparam>
        /// <param name="leftTransform">
        /// Function that will be called if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsLeft" />.
        /// </param>
        /// <param name="rightTransform">
        /// Function that will be called if current instance of <see cref="IEither{TLeft,TRight}" />.
        /// <see cref="IsRight" />.
        /// </param>
        /// <returns>Joined value transformed from any value that is contained by this instance of <see cref="IEither{TLeft,TRight}"/>.</returns>
        TResult Join<TResult>(Func<TLeft, TResult> leftTransform, Func<TRight, TResult> rightTransform);
    }
}