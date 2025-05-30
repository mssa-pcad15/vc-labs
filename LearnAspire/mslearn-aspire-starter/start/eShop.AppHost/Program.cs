var builder = DistributedApplication.CreateBuilder(args);

// Databases

//var basketStore = builder.AddRedis("BasketStore").WithRedisCommander();
var mongo = builder.AddMongoDB("mongo")
  .WithMongoExpress()
  .AddDatabase("BasketDB");

//postgres service
var postgres = builder.AddPostgres("postgres")
    .WithEnvironment("POSTGRES_DB", "CatalogDB")
    .WithBindMount("../Catalog.API/Seed", "/docker-entrypoint-initdb.d").WithPgAdmin();

//add a database "CatalogDB" to the postgres service
var catalogDB = postgres.AddDatabase("CatalogDB");

// Identity Providers

var idp = builder.AddKeycloakContainer("idp", tag: "23.0")
    .ImportRealms("../Keycloak/data/import");

// DB Manager Apps

// API Apps

var catalogApi = builder.AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(catalogDB);

var basketApi = builder.AddProject<Projects.Basket_API>("basket-api")
        .WithReference(mongo)
        .WithReference(idp);

// Apps

// Force HTTPS profile for web app (required for OIDC operations)
var webApp = builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(catalogApi)
    .WithReference(basketApi)
    .WithExternalHttpEndpoints()
    .WithReference(idp, env: "Identity__ClientSecret");

// Inject the project URLs for Keycloak realm configuration
var webAppHttp = webApp.GetEndpoint("http");
var webAppHttps = webApp.GetEndpoint("https");
idp.WithEnvironment("WEBAPP_HTTP_CONTAINERHOST", webAppHttp);
idp.WithEnvironment("WEBAPP_HTTP", () => $"{webAppHttp.Scheme}://{webAppHttp.Host}:{webAppHttp.Port}");
if (webAppHttps.Exists)
{
  idp.WithEnvironment("WEBAPP_HTTPS_CONTAINERHOST", webAppHttps);
  idp.WithEnvironment("WEBAPP_HTTPS", () => $"{webAppHttps.Scheme}://{webAppHttps.Host}:{webAppHttps.Port}");
}
else
{
  // Still need to set these environment variables so the KeyCloak realm import doesn't fail
  idp.WithEnvironment("WEBAPP_HTTPS_CONTAINERHOST", webAppHttp);
  idp.WithEnvironment("WEBAPP_HTTPS", () => $"{webAppHttp.Scheme}://{webAppHttp.Host}:{webAppHttp.Port}");
}

// Inject assigned URLs for Catalog API
catalogApi.WithEnvironment("CatalogOptions__PicBaseAddress", catalogApi.GetEndpoint("http"));

builder.Build().Run();
