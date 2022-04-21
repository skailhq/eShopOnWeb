export ASPNETCORE_ENVIRONMENT=Development
export SIDECAR=dev-nomad-01.skail.me:5003
export SkailPlatform__Logging__LogLevel=Default
export NAMESPACE=eShopOnWeb
export IMAGE=oci://registry.digitalocean.com/skail/func/accountvalidation:v0.0.1
export BATCH_SIZE=1
export SKAIL_FUNC=true
export SMTP_SERVER=137.184.101.250:2525
export WAIT_SECONDS=5
export SkailPlatform__Remote__0__TypeQualifiedName="Microsoft.eShopWeb.Infrastructure.Services.IAccountValidation"
export SkailPlatform__Remote__0__ImageUri="oci://registry.digitalocean.com/skail/func/accountvalidation:v0.0.1"
export ConnectionStrings__IdentityConnection="Server=137.184.101.250,1433;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.Identity;User Id=sa;Password=@someThingComplicated1234;Trusted_Connection=false;"
export DOTNET_ROOT=/usr/share/dotnet    

cd /app
./skail_func