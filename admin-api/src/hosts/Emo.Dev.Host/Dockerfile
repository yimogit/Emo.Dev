FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY ["/src/hosts/Emo.Dev.Host/Emo.Dev.Host.csproj", "/src/hosts/Emo.Dev.Host/Emo.Dev.Host.csproj"]
RUN dotnet restore "/src/hosts/Emo.Dev.Host/Emo.Dev.Host.csproj" -nowarn:NETSDK1138
COPY . .
WORKDIR "/src/src/hosts/Emo.Dev.Host"
ENV NUGET_XMLDOC_MODE none
RUN dotnet publish "Emo.Dev.Host.csproj" -nowarn:NETSDK1138 -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8000
ENTRYPOINT ["dotnet", "Emo.Dev.Host.dll"]