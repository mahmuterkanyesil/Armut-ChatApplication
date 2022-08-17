FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_EVNIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChatApplication.API/ChatApplication.API.csproj", "ChatApplication.API/"]
RUN dotnet restore "ChatApplication.API/ChatApplication.API.csproj"
COPY . .
WORKDIR "/src/ChatApplication.API"
RUN dotnet build "ChatApplication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatApplication.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatApplication.API.dll","--environment=Development"]