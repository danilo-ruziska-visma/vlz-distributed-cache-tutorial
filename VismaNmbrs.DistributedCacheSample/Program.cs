using VismaNmbrs.DistributedCacheSample.Data;
using VismaNmbrs.DistributedCacheSample.Data.Extensions;
using VismaNmbrs.DistributedCacheSample.Cache;
using VismaNmbrs.DistributedCacheSample.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<CacheSettingsOptions>(options =>
{    
    options.DefaultSlidingExpirationInMinutes = int.Parse(builder.Configuration.GetSection("CacheSettings")["DefaultSlidingExpirationInMinutes"]);
});

// This method is not native. It was created in the VismaNmbrs.DistributedCacheSample.Data class library
builder.Services.AddAzureCosmo(options =>
{
    options.EndpointUri = builder.Configuration.GetSection("AzureCosmo")["EndpointUri"];
    options.PrimaryKey = builder.Configuration.GetSection("AzureCosmo")["PrimaryKey"];
});

builder.Services.AddScoped<ICacheProvider, LocalCacheProvider>();

builder.Services.AddAutoMapper(typeof(Program));

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
