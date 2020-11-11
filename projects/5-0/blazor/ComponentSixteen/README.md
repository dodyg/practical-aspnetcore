# Cascading value by type

This sample demonstrates cascading value by type feature. This is a parameter that get passed through a component without having to explicitly assign them.

All the parameters that share the same type will share the same value.

`Name` and `Location` would share the same value because they are both `string`.

```
    [CascadingParameter]
    private string Name { get; set;}

    [CascadingParameter]
    private string Location { get; set;}

```