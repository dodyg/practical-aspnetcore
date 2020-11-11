# Cascading value by name

This sample demonstrates cascading value by name feature. This is a parameter that get passed through a component without having to explicitly assign them. You can pass primitives as well as complex object this way. 

In this sample you will see how cascading values flow to all components.

In cascading value by type, all cascading parameters of the same type will share the same value. In cascading value by name, only parameters that match with the assigned name (and type) will receive it.

Below code is valid for example. Person and Person2 properties will receive the same value.

```
    [CascadingParameter(Name = "PersonInfo")]
    private Person Person { get; set;}

    
    [CascadingParameter(Name = "PersonInfo")]
    private Person Person2 { get; set;}
```


