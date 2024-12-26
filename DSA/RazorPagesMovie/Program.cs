
using Azure.Data.Tables;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string connstring = "DefaultEndpointsProtocol=https;AccountName=vc0316;AccountKey=49DVOVjSX96zUM6jgg0vnPSECKWQCOibRiJCt4ggSVpGBMJqUbboY9y7iy73kEm8ktaiveBa7PPQ+AStLTd1Tg==;EndpointSuffix=core.windows.net";
string tableName = "counter";

builder.Services.AddSingleton(
    typeof(TableClient),
    new TableClient(connstring, tableName)
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
builder.Services.AddSingleton<Menu>(new Menu { ProteinOfTheDay = "Chicken", SoupOfTheDay = "Tomato" });

// singleton:
// con: scalability - because the same instance MUST be available -- how do we scale across machines?
WebApplication ? app = builder.Build();

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
