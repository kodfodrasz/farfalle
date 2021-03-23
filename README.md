# Farfalle

![Farfalle logo](Farfalle/wwwroot/images/farfalle-256px.png)

A simple copy-pasta web-application.

## How to run locally

Ensure the following prerequisites are installed 

 - .Net Core 5.0
 - npm
 - yarn

Build the project:

~~~sh
# 1. Restore dotnet tools (eg. Fable):
dotnet tool restore
# 2. Compile the project
dotnet build
~~~

3. Run it!

~~~sh
export FARFALLE_DATA_DIR=/some/path/to/put/uploads/
dotnet run
~~~

The app should be up and running. You can try the app at <http://localhost:5001>.

## Acknowledgements

### Logo

Logo made by [Freepik](https://www.flaticon.com/authors/freepik), downloaded from [flaticon.com](https://www.flaticon.com/free-icon/farfalle_4162121).

### Icons

Icons from the following authors are used:

 - <a href="https://www.flaticon.com/authors/dinosoftlabs" title="DinosoftLabs">DinosoftLabs</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a>

### Third party libraries

 - [Falco](https://github.com/pimbrouwers/Falco) (Apache License 2.0)
 - [FSharp.Core](https://github.com/dotnet/fsharp) (MIT License)
 - [Microsoft ASP.Net Core](https://github.com/dotnet/aspnetcore) (Apache License 2.0)
 - [NLipsum](https://github.com/alexcpendleton/NLipsum) (MIT License)
