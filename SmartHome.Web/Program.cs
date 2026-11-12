using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Data;
using SmartHome.Web.Services;
using SmartHome.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Липсва connection string с име 'DefaultConnection' в appsettings.json.");

builder.Services.AddDbContext<SmartHomeDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddControllersWithViews(options =>
{
    
    var messages = options.ModelBindingMessageProvider;
    messages.SetValueMustNotBeNullAccessor(_ => "Полето е задължително.");
    messages.SetValueMustBeANumberAccessor(field => $"Полето „{field}“ трябва да бъде число.");
    messages.SetAttemptedValueIsInvalidAccessor((value, field) => $"Стойността „{value}“ не е валидна за „{field}“.");
    messages.SetValueIsInvalidAccessor(value => $"Стойността „{value}“ е невалидна.");
    messages.SetUnknownValueIsInvalidAccessor(field => $"Въведената стойност за „{field}“ е невалидна.");
    messages.SetMissingBindRequiredValueAccessor(field => $"Липсва стойност за „{field}“.");
    messages.SetMissingKeyOrValueAccessor(() => "Стойността е задължителна.");
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
    dbContext.Database.Migrate();
}


var culture = (CultureInfo)CultureInfo.GetCultureInfo("bg-BG").Clone();
culture.NumberFormat.NumberDecimalSeparator = ".";
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = new List<CultureInfo> { culture },
    SupportedUICultures = new List<CultureInfo> { culture }
};
localizationOptions.RequestCultureProviders.Clear();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();