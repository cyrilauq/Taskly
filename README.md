# About the project

Project named "Taskly" for todo list management.

This project will contains three app to manage a user todos:
- an api in ASP.NET
- a web client using .NET Blazor WASM
- a mobile client using .NET MAUI

In this project, I will explore many things like:
- new design patterns

# Projects architecture
## API

The api will be build using two main design patterns: Clean Architecture and CQRS.

## Start with the project
On your compture you should
- create a directory "LocalNuget" in the disk "C:\"
- create a new nuget source "LocalNuget" that use the previously created directory as its path
- create a package of the project "Taskly.Client.Domain"
- push the create package on the "LocalNuget" source