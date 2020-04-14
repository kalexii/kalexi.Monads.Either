using System;
using System.Diagnostics.CodeAnalysis;
using kalexi.Monads.Either.Code;
using kalexi.Monads.Either.Exceptions;
using NUnit.Framework;

namespace kalexi.Monads.Either.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "TestFileNameWarning")]
    public class RightEitherTests
    {
        private const string value = "left";

        [Test]
        public void CreatesByConstructor() => ValidateRightEither(new Either<int, string>(value));

        [Test]
        public void CreatesByImplicitCast() => ValidateRightEither(value);

        [Test]
        public void CreatesByStaticMethod()
            => ValidateRightEitherInterface(Either<int, string>.CreateRight(value));

        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        private static void ValidateRightEither(Either<int, string> either) => ValidateRightEitherInterface(either);

        private static void ValidateRightEitherInterface(IEither<int, string> either)
        {
            AssertIsValidRightEither(either, value);
            AssertDoWithRightAction(either);
            AssertDoWithRightFunction(either);
            AssertDidNotDoWithLeft(either);
            AssertSwitchedRight(either);
            AssertMapRight(either);
            AssertNonAlteringMapRight(either);
            AssertJoin(either);
        }

        private static void AssertJoin(IEither<int, string> either)
            => Assert.That(either.Join(x => 1338, x => 1337), Is.EqualTo(1337));

        private static void AssertDoWithRightFunction(IEither<int, string> either)
            => Assert.That(either.DoWithRight(s => s), Is.EqualTo(value));

        private static void AssertNonAlteringMapRight(IEither<int, string> either)
        {
            var mapResult = either.NonAlteringMap(v => 1337, v => "1337");
            AssertIsValidRightEither(mapResult, "1337");
        }

        private static void AssertMapRight(IEither<int, string> either)
        {
            var mapResult = either.Map(v => 1337, v => "1337");
            AssertIsValidRightEither(mapResult, "1337");
        }

        private static void AssertSwitchedRight(IEither<int, string> either)
        {
            var switchedLeft = false;
            var switchedRight = false;
            either.Switch(v => switchedLeft = true, v => switchedRight = true);
            Assert.That(switchedLeft, Is.False);
            Assert.That(switchedRight, Is.True);
        }

        private static void AssertDidNotDoWithLeft(IEither<int, string> either)
        {
            var didWithLeft = false;
            either.DoWithLeft(i => didWithLeft = true);
            Assert.That(didWithLeft, Is.False);
            Assert.That(either.DoWithLeft(i => i, 1337), Is.EqualTo(1337));
        }

        private static void AssertDoWithRightAction(IEither<int, string> either)
        {
            var didWithRight = false;
            either.DoWithRight(v => { didWithRight = true; });
            Assert.That(didWithRight, Is.True);
        }

        private static void AssertIsValidRightEither(IEither<int, string> either, string expectedValue)
        {
            Assert.That(either, Is.Not.Null);
            Assert.That(either.IsRight, Is.True);
            Assert.That(either.Right, Is.EqualTo(expectedValue));
            Assert.That(either.IsLeft, Is.False);
            Assert.Throws<EitherException>(() => Console.WriteLine(either.Left));
        }
    }
}