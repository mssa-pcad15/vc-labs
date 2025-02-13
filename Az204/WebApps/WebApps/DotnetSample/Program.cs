using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);

if (Uri.TryCreate(builder.Configuration["AppConfig"], UriKind.Absolute, out var endpoint))
{
    // Use Azure Active Directory authentication.
    // The identity of this app should be assigned 'App Configuration Data Reader' or 'App Configuration Data Owner' role in App Configuration.
    // For more information, please visit https://aka.ms/vs/azure-app-configuration/concept-enable-rbac

    if (builder.Configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
    {
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            options.Select(keyFilter: "*", "local");
            options.Connect(builder.Configuration["AppConfig_ConnectionString"])
           .ConfigureRefresh(refresh =>
            {
                // All configuration values will be refreshed if the sentinel key changes.
                refresh.Register("TestApp:Settings:Sentinel", refreshAll: true);
            });
        });
    }
    else //i am running in azure
    {
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(endpoint, new ManagedIdentityCredential())
               .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()))
                .ConfigureRefresh(refresh =>
                {
                    // All configuration values will be refreshed if the sentinel key changes.
                    refresh.Register("TestApp:Settings:Sentinel", refreshAll: true);
                });
        });
    }
}
builder.Services.AddAzureAppConfiguration();


// Add services to the container.
builder.Services.AddRazorPages();

//Configuration Challenge
// appsettings.json
// appsettings.{based on ASPNETCORE_ENVIRONMENT env variable}.json
// webapp in azure
// azure key vault
// azure configuration service


var app = builder.Build();


//begin middleware section
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//middlewares
app.UseHttpsRedirection()
    .UseAzureAppConfiguration()
    .UseRouting()
    .UseAuthorization();



app.UseStaticFiles();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
