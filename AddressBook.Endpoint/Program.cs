using Infra.Context;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SqliteDbContext>(optionsAction => optionsAction.UseSqlite());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<SqliteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IContactRepository, ContactRepository>();
var app = builder.Build();
var services = new ServiceCollection()
    .AddLogging(loggingBuilder => { loggingBuilder.AddConsole(); })
    .AddSingleton(app)
    .BuildServiceProvider();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllerRoute(
    "default",
    "{controller}/{action=Index}/{id?}");
app.MapControllers();
app.Run();