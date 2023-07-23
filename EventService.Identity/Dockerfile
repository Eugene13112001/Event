FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY /bin/Release/net7.0/publish/ .
EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "EventService.Identity.dll"]