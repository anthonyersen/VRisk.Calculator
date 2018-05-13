FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY VRisk.Calculator.Api/VRisk.Calculator.Api.csproj VRisk.Calculator.Api/
RUN dotnet restore VRisk.Calculator.Api/VRisk.Calculator.Api.csproj
COPY . .
WORKDIR /src/VRisk.Calculator.Api
RUN dotnet build VRisk.Calculator.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish VRisk.Calculator.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VRisk.Calculator.Api.dll"]
