# OpenAPI Multiple `Produces<T>` per status code

An endpoint can declare several `Produces<T>()` for the same status code and the OpenAPI document emits a
separate content entry per media type or an `anyOf` schema when multiple types share a content type.
