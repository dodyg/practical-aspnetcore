# Hello World with Redis storage

This is the hello world of an Orleans system (["Orleans is a cross-platform framework for building robust, scalable distributed applications"](https://github.com/dotnet/orleans)). 

- Make sure you have redis installed and running.  
- Run `dotnet run` on `silo` folder.
- Run `dotnet run` on `client` folder. Close and run it again multiple times. You will see that one of the Hello World Grain returns more greetings in every run. 
- Then stop the `client` and the `silo`.
- Then start the `silo` and the `client` again. You will see that the greetings are persisted between the restarts of the `silo`.
