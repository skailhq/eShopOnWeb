ASPNETCORE_ENVIRONMENT=Development
SIDECAR=dev-nomad-01.skail.me:5003
SkailPlatform__Logging__LogLevel=Default
NAMESPACE=eShopOnWeb
IMAGE=oci://registry.digitalocean.com/skail/func/accountvalidation:v0.0.1
BATCH_SIZE=1
SKAIL_FUNC=true
SMTP_SERVER=137.184.101.250:2525
WAIT_SECONDS=5
SkailPlatform__Remote__0__TypeQualifiedName="Microsoft.eShopWeb.Infrastructure.Services.IAccountValidation"
SkailPlatform__Remote__0__ImageUri="oci://registry.digitalocean.com/skail/func/accountvalidation:v0.0.1"
ConnectionStrings__IdentityConnection="Server=137.184.101.250,1433;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.Identity;User Id=sa;Password=@someThingComplicated1234;Trusted_Connection=false;"
DOTNET_ROOT=/usr/share/dotnet