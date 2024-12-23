
using RazorPagesMovie;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(typeof(Counter), new Counter());
//AddSingleton is used for "GLOBAL" "STATEFUL" service
//lottery history
//leaderboard
//cache service
//step 2, add Menu to DI Container
//line 15 is the same as line 8, just use a shorter version of syntax
builder.Services.AddSingleton<Menu>(new Menu { ProteinOfTheDay = "Chicken", SoupOfTheDay = "Tomato" });

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
