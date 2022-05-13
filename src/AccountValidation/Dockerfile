FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG feed_name
ARG feed_user
ARG feed_password
WORKDIR /src

RUN dotnet nuget add source https://pkgs.dev.azure.com/virtualgroup/Plataforma/_packaging/skail-dev/nuget/v3/index.json --name $feed_name --username $feed_user --password $feed_password --store-password-in-clear-text


COPY ./ApplicationCore/ ./ApplicationCore/
RUN dotnet build ./ApplicationCore/ApplicationCore.csproj \
    -c Release \
    -r alpine-x64 \
    --no-dependencies \
    --restore \
    --self-contained true

COPY ./SendEmail/ ./SendEmail/
RUN dotnet build ./SendEmail/SendEmail.csproj \
    -c Release \
    -r alpine-x64 \
    --no-dependencies \
    --restore \
    --self-contained true
    
COPY ./Infrastructure/ ./Infrastructure/
RUN dotnet build ./Infrastructure/Infrastructure.csproj \
    -c Release \
    -r alpine-x64 \
    --restore \
    --self-contained true

COPY ./AccountValidation/ ./AccountValidation/
RUN dotnet build ./AccountValidation/AccountValidation.csproj \
    -c Release \
    -p:UseAppHost=true \
    -r alpine-x64 \
    --restore \
    --self-contained true

RUN dotnet publish ./AccountValidation/AccountValidation.csproj \
    -p:PublishSingleFile=true \
    -p:PublishReadyToRun=false \
    -p:ErrorOnDuplicatePublishOutputFiles=false \
    -r alpine-x64 \
    -c Release \
    --self-contained true \
    --no-dependencies \
    --no-restore \
    -o /skail/app 

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine as runtime
WORKDIR /
RUN apk add icu-libs
COPY --from=build /skail/app/AccountValidation ./app/skail_func

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT=Development
ENV SIDECAR=dev-nomad-01.skail.me:5003
ENV SkailPlatform__Logging__LogLevel=Default
ENV NAMESPACE=eShopOnWeb
ENV IMAGE=oci://registry.digitalocean.com/skail/eshoppingweb/accountvalidation:v0.0.1
ENV BATCH_SIZE=1
ENV SKAIL_FUNC=true
ENV SMTP_SERVER=137.184.101.250:2525
ENV WAIT_SECONDS=5
ENV SkailPlatform__Remote__0__TypeQualifiedName="Microsoft.eShopWeb.Infrastructure.Services.IAccountValidation"
ENV SkailPlatform__Remote__0__ImageUri="oci://registry.digitalocean.com/skail/eshoppingweb/accountvalidation:v0.0.1"
ENV ConnectionStrings__IdentityConnection="Server=137.184.101.250,1433;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.Identity;User Id=sa;Password=@someThingComplicated1234;Trusted_Connection=false;"
ENV DOTNET_ROOT=/usr/share/dotnet  

ENTRYPOINT "/app/skail_func"
