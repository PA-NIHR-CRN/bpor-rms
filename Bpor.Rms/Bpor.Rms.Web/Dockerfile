FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bpor.Rms.Web/Bpor.Rms.Web.csproj", "Bpor.Rms.Web/"]
RUN dotnet restore "Bpor.Rms.Web/Bpor.Rms.Web.csproj"
COPY . .
WORKDIR "/src/Bpor.Rms.Web"
RUN dotnet build "Bpor.Rms.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bpor.Rms.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bpor.Rms.Web.dll"]
