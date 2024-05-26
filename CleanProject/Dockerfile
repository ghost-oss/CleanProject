#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Every dockerfile requires a base image (FROM). This is a base .net core image that we will build our .net core application on. The base image can be found on DockerHub.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

#Telling docker that this particular container will listen on port 80 (container internal port)
#When it comes to 'docker run' we have to link a desired external port to the containers port via -p. For example, docker run -p 8080(host external port):80(container port)
#The -p process (mapping ports) is referred to as publishing
EXPOSE 80

#Go into our src folder and copy our project Dlls into the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CleanProject/CleanProject.API.csproj", "CleanProject/"]
COPY ["CleanProject.Application/CleanProject.Application.csproj", "CleanProject.Application/"]
COPY ["CleanProject.Domain/CleanProject.Domain.csproj", "CleanProject.Domain/"]
COPY ["CleanProject.Persistance/CleanProject.Persistance.csproj", "CleanProject.Persistance/"]

#Restore/install any dependacies within our project and copy that back into the /src folder
RUN dotnet restore "CleanProject/CleanProject.API.csproj"
COPY . .

#Go into the clean project directory and build our app. -c = release mode -o = output. So build our app and add it within /app/build folder
WORKDIR "/src/CleanProject"
RUN dotnet build "CleanProject.API.csproj" -c Release -o /app/build

#Now take the build and publish it into the /app/publish folder
FROM build AS publish
RUN dotnet publish "CleanProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Switch to /app directory and copy the published build that is in /app/publish i.e the binaries
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#Specifcy our entry point for the application in this case which DLL is the entry point
ENTRYPOINT ["dotnet", "CleanProject.dll"]