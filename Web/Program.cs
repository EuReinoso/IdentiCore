using Application.Services;
using Web.Integration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddScoped<IdentiCoreIntegration>();
builder.Services.AddHttpClient<IdentiCoreIntegration>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7272/api/");
}).AddHttpMessageHandler<AuthHeaderHandler>(); ;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clients}/{action=Login}/{id?}");

app.Run();
