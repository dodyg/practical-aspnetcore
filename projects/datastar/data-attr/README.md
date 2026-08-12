# data-attr

This example shows how to use `data-attr` to set HTML attribute to an expression. This sample demonstrates the usage of direct value, object and also signal expression.

Make sure that you use a single quote `'` when you are assigning string value otherwise you will get an error.

``` html
 <h1 data-attr:title="'Data Atttribute Page'">data-atttr</h1>
 ```

 ``` html
   <body class="container" data-signals="{btn1 : 'Normal Button', btn2 : true}">

   <div><button data-attr:disabled="$btn2">Disabled Button</button></div>
```   

``` html 
   <div data-attr="{ width: '50px',  height:'50px', style:'border:1px solid red;' }"></div>
```   