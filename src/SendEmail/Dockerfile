FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG feed_name
ARG feed_user
ARG feed_password
WORKDIR /src

RUN dotnet nuget add source https://pkgs.dev.azure.com/virtualgroup/Plataforma/_packaging/skail-dev/nuget/v3/index.json --name $feed_name --username $feed_user --password $feed_password --store-password-in-clear-text

COPY ./SendEmail/SendEmail.csproj ./SendEmail/SendEmail.csproj
RUN dotnet restore ./SendEmail/SendEmail.csproj
COPY ./SendEmail ./SendEmail

RUN dotnet restore ./SendEmail/SendEmail.csproj

RUN dotnet publish ./SendEmail/SendEmail.csproj \
        -p:PublishSingleFile=true \
        -p:PublishReadyToRun=true \
        -r alpine-x64 \
        -c Release \
        --self-contained true \
        -o /skail/app 

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine as runtime
WORKDIR /
COPY --from=build /skail/app/SendEmail ./app/skail_func
ENTRYPOINT "/app/skail_func"



