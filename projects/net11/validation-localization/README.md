# Localized validation messages

`AddValidation()` + `AddLocalization()` now localize validation messages and display names automatically. The keys are emitted by the validation source generator; a miss falls back to the attribute's built-in message.

This sample registers a custom `IStringLocalizerFactory` backed by an in-memory dictionary (English/Spanish). 

## HTML form with a culture switcher
