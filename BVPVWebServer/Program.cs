using BVPVWebServer.Services.Admin;
using BVPVWebServer.Services.General;
using BVPVWebServer.Services.Water;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();

builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AppService>();
builder.Services.AddScoped<MarkdownService>();
builder.Services.AddScoped<DownloadService>();
builder.Services.AddScoped<WaterDatService>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();