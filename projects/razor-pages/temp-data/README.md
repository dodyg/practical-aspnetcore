# Use TempData backed by cookies

By default `TempData` in Razor Pages is backed by cookies. This sample shows how to use `TempData` in a Razor Pages app using `[TempData]` attribute.

Remember that when you access `TempData` value, it will disappear. To retain the value in `TempData`, use, `TempData.Peek`.  