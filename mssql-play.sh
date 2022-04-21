#!/bin/bash
#podman run -e SA_PASSWORD=@someThingComplicated1234 -e ACCEPT_EULA=Y --rm=true -p 1433:1433/tcp mcr.microsoft.com/azure-sql-edge:latest &
#sleep 30

cd src/Web
dotnet restore
dotnet tool restore
dotnet ef database update -c catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj --configuration Podman
dotnet ef database update -c appidentitydbcontext  -p ../Infrastructure/Infrastructure.csproj -s Web.csproj --configuration Podman