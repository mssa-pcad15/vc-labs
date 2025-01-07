
using Azure.Data.Tables;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie;
using Microsoft.Extensions.Configuration;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddRazorPages();
        
        //read the ConnectionString and TableName from built-in ConfigurationService
        builder.Services.AddSingleton(
            typeof(TableClient),
            new TableClient(builder.Configuration["ConnectionString"], builder.Configuration["TableName"]) // this works!!
            );

       //builder.Services.AddTransient(typeof(Counter), typeof(DistributedCounter));
        builder.Services.AddSingleton(typeof(Counter), typeof(SingletonDistributedCounter));
        //step 5 : comment out line 20 and replace with AddSingleton

        //AddSingleton is used for "GLOBAL" "STATEFUL" service
        //lottery history
        //leaderboard
        //cache service
        //step 2, add Menu to DI Container
        //line 15 is the same as line 8, just use a shorter version of syntax
        builder.Services.AddSingleton(new Menu { ProteinOfTheDay = "Chicken", SoupOfTheDay = "Tomato" });

        // singleton:
        // con: scalability - because the same instance MUST be available -- how do we scale across machines?
        WebApplication? app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}