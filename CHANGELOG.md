# Moon.Validation changelog

7.1.2

- Ensures compatibility with latest Moon.* libraries.

7.1.1

- All libraries are targeting .NET Standard (except `Moon.Windows.Validation`).

7.0.2

- Removed unnecessary dependencies.
- Added `RequiredEmpty` validator that can be used to prevent spam.
- The `DataValidator` can recursively validate `IEnumerable` items.
- Fixed issues with circular references and primitive types.

7.0.1

- Asynchronous validation (`IAsyncValidatableObject`) has been removed.
- Recursive validation has been added.

6.0.8

- You can now use common validation messages. Simply ommit the property name in resource key (eg. `_Required`).

6.0.7

- Unified behavior of `DataValidator` and ASP.NET Core integration.

6.0.2

- `Moon.Validation` project migrated to .NET Standard 1.3.

6.0.1

- Support for OWIN ASP.NET MVC applications has been dropped.
- All `ITextProvider` implementations are deprecated and no longer supported.
- Attribute localization is now based on [IStringLocalizer](https://docs.asp.net/projects/api/en/latest/autoapi/Microsoft/Extensions/Localization/IStringLocalizer/) introduced in ASP.NET Core.
- ASP.NET Core integration has been rewritten to support 1.0 RTM version.
- An `IAsyncValidatableObject` has been added to support async object validation.
- The `DataValidator` can't valdiate specific properties anymore.