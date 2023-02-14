using Infra.Context;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;


// Configuration
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddDbContext<SqliteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AddressBookDbName")));
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IAddressBookService, AddressBookService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7096/AddressBook/");
});
var app = builder.Build();
// Services
// var services = new ServiceCollection()
//     .AddLogging(loggingBuilder => { loggingBuilder.AddConsole(); })
//     .AddSingleton(app)
//     .AddDbContext<SqliteDbContext>(options =>
//         options.UseSqlite(builder.Configuration.GetConnectionString("AddressBookDbName")))
//     .AddSingleton<IAddressBookService, AddressBookService>()
//     .AddSingleton<IContactRepository, ContactRepository>()
//     .BuildServiceProvider();
//
// var logger = (services.GetService<ILoggerFactory>() ?? throw new InvalidOperationException())
//     .CreateLogger<Program>();

// logger.LogInformation($@" Starting application at: {DateTime.Now}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();