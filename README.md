# kalexi.Monads.Either
Implementation of Either monad.

[![Build status](https://ci.appveyor.com/api/projects/status/26rx8d8bu7bgcu06?svg=true)](https://ci.appveyor.com/project/kalexii/kalexi-monads-either)

Either monad encapsulates the outcome that can be of two different types. Either TLeft, or TRight.

Example usage:
```csharp
  public Either<T1, T2> MethodThatCouldProduceMeaningfulValuesOfMultipleTypes<T1, T2>() {
    return someCondition ? ProduceValueOfType<T1>() : ProduceValueOfType<T2>();
  }
```

Implementation notes:
- Instances of `Either<TLeft, TRight>` don't do any condition checking (am I left or right?). Instead, they delegate everything to their private field `eitherState` that implements `IEither<TLeft, TRight>` interface. This interface is implemented by these two guys here - `LeftEitherState<TLeft, TRight>` and `RightEitherState<TLeft, TRight>`. Upon creation of `Either<TLeft, Right>` a corresponding `IEither<TLeft, TRight>` implementation is created, stored and used thenceforth. You could call it a state machine of sort.
- Implicit operators allow implicit casting of value of type `TLeft` or `TRight` to `Either<TLeft, TRight>`. Example:
```csharp
  public Either<string, int> DoSomething() {
    if (someCondition) {
      return "some string value";
    } else {
      return 1;
    }
  }
```

Todo:
 - Try implementing as a struct with if's and see if it will perform better.
 - Expand on this document.
 - Embed docs and debugging symbols into the nuget package.
