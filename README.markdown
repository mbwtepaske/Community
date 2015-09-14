#Community

*Community library adding useful utilities and extensions.*

##Services, Providers & Helpers:
 - ActionDisposable     : Disposable 
 - Assert			          : Useful for argument validation
 - TypeConverterService	: Used for easy access to type converters and caching system

##Extensions:
  - ICloneable
    - `T Clone<T>()`
  - IComparable
    - `T Clamp<T>(T? minimum = null, T? maximum = null) where T : struct, IComparable`
  - IDictionary
    - `TValue GetValueOrDefault<TKey, TValue>(TKey key)`
    - `TValue GetValueOrDefault<TKey, TValue>(TKey key, TValue value)`
    - `TValue GetValueOrDefault<TKey, TValue>(TKey key, Func<TKey, TValue> valueFactory)`
  - IEnumerable
  - Stream
  - String
  - XDocument
  - XElement
  - XPathNavigator