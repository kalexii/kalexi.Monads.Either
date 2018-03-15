using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using kalexi.Monads.Either.Code;
using kalexi.Monads.Either.Exceptions;

namespace kalexi.Monads.Either.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "TestFileNameWarning")]
    public class LeftEitherTests
    {
        private const string value = "left";

        [Test]
        public void CreatesByConstructor() => ValidateLeftEither(new Either<string, int>(value));

        [Test]
        public void CreatesByImplicitCast() => ValidateLeftEither(value);

        [Test]
        public void CreatesByStaticMethod()
            => ValidateLeftEitherInterface(Either<string, int>.CreateLeft(value));

        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        private void ValidateLeftEither(Either<string, int> either) => ValidateLeftEitherInterface(either);

        private void ValidateLeftEitherInterface(IEither<string, int> either)
        {
            AssertIsValidLeftEither(either, value);
            AssertDoWithLeftAction(either);
            AssertDoWithLeftFunction(either);
            AssertDidNotDoWithRight(either);
            AssertSwitchedLeft(either);
            AssertMapLeft(either);
            AssertNonAlteringMapLeft(either);
        }

        private static void AssertDoWithLeftFunction(IEither<string, int> either) 
            => Assert.That(either.DoWithLeft(s => s), Is.EqualTo(value));

        private static void AssertNonAlteringMapLeft(IEither<string, int> either)
        {
            var mapResult = either.NonAlteringMap(v => "1337", v => 1337);
            AssertIsValidLeftEither(mapResult, "1337");
        }

        private static void AssertMapLeft(IEither<string, int> either)
        {
            var mapResult = either.Map(v => "1337", v => 1337);
            AssertIsValidLeftEither(mapResult, "1337");
        }

        private static void AssertSwitchedLeft(IEither<string, int> either)
        {
            var switchedLeft = false;
            var switchedRight = false;
            either.Switch(v => switchedLeft = true, v => switchedRight = true);
            Assert.That(switchedLeft, Is.True);
            Assert.That(switchedRight, Is.False);
        }

        private static void AssertDidNotDoWithRight(IEither<string, int> either)
        {
            var didWithRight = false;
            either.DoWithRight(i => didWithRight = true);
            Assert.That(didWithRight, Is.False);
            Assert.That(either.DoWithRight(i => i, 1337), Is.EqualTo(1337));
        }

        private static void AssertDoWithLeftAction(IEither<string, int> either)
        {
            var didWithLeft = false;
            either.DoWithLeft(v => { didWithLeft = true; });
            Assert.That(didWithLeft, Is.True);
        }

        private static void AssertIsValidLeftEither(IEither<string, int> either, string expectedValue)
        {
            Assert.That(either, Is.Not.Null);
            Assert.That(either.IsLeft, Is.True);
            Assert.That(either.Left, Is.EqualTo(expectedValue));
            Assert.That(either.IsRight, Is.False);
            Assert.Throws<EitherException>(() => Console.WriteLine(either.Right));
        }
    }
}
