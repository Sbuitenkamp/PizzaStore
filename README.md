# PizzaStore
Design patterns: 
 - composite
 - singleton
 - visitor

Secure Design Pattern: 
 - input validation
 - message inspection

## Running the web socket server
Run the command
```sh
dotnet watch run --project ./Web/WebSocketServer/WebSocketServer.csproj
```
`watch` is only neccessary for development.

## Running the web server
Open another terminal and run the command
```sh
dotnet watch run --project ./Client/PizzaStore.csproj
```
`watch` is only neccessary for development.
