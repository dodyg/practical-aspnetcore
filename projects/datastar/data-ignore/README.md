# data-ignore

This example shows to use `data-ignore` to tell Datastar to skip an element and its descendants. 

``` html
    <div data-ignore><button data-on:click="$count = $count + 1;" data-text="$count">Under data-ignore</button></div>
    <div><button data-on:click="$count = $count + 1;" data-text="$count"></button></div>
```