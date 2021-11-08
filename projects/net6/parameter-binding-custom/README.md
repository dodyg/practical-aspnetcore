# Custom binding to types over Route, Query or Header

This shows how to bind custom types over Route, Query or Header.

Your type must implement either of this static methods:
``` csharp
public static bool TryParse(string value, T out result);
public static bool TryParse(string value, IFormatProvider provider, T out result);
```