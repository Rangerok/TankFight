FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ImageService/ImageService.csproj .
RUN dotnet restore ImageService.csproj
COPY ImageService .
RUN dotnet build ImageService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ImageService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ImageService.dll"]