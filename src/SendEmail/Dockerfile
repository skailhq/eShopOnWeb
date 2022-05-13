FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG feed_name
ARG feed_user
ARG feed_password
WORKDIR /src

RUN dotnet nuget add source https://pkgs.dev.azure.com/virtualgroup/Plataforma/_packaging/skail-dev/nuget/v3/index.json --name $feed_name --username $feed_user --password $feed_password --store-password-in-clear-text

COPY ./ ./

RUN dotnet publish ./SendEmail.csproj \
        -p:PublishSingleFile=true \
        -p:PublishReadyToRun=true \
        -r alpine-x64 \
        -c Release \
        --self-contained true \
        -o /skail/

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine as runtime
WORKDIR /
RUN apk add icu-libs
COPY --from=build /skail/SendEmail ./app/skail_func

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV SIDECAR="dev-nomad-01.skail.me:5003"
ENV SkailPlatform__Logging__LogLevel="Default"
ENV NAMESPACE="eShopOnWeb"
ENV IMAGE="oci://registry.digitalocean.com/skail/eshoppingweb/sendemail:v0.0.1"
ENV BATCH_SIZE="1"
ENV SMTP_SERVER="137.184.101.250:2525"

ENTRYPOINT "/app/skail_func"



