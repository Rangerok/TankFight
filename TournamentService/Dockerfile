FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY TournamentService/TournamentService.csproj .
RUN dotnet restore TournamentService.csproj
COPY TournamentService .
RUN dotnet build TournamentService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TournamentService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TournamentService.dll"]