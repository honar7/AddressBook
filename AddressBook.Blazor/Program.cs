using Infra.Repository;
// Configuration
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IAddressBookRepository, AddressBookRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7096/AddressBook/");
});
var app = builder.Build();
// Services
var services = new ServiceCollection()
    .AddLogging(loggingBuilder => { loggingBuilder.AddConsole(); })
    .AddSingleton(app)
    .AddSingleton<IAddressBookRepository, AddressBookRepository>()
    .BuildServiceProvider();

var logger = (services.GetService<ILoggerFactory>() ?? throw new InvalidOperationException())
    .CreateLogger<Program>();

logger.LogInformation($@" Starting application at: {DateTime.Now}");

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