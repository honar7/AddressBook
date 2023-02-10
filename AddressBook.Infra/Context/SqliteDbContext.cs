using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Contact>().HasData(new Contact
        {
            ContactId = 1,
            Name = "John Doe",
            Email = "email@doe.com",
            MobilePhone = "+49 1522 3433333",
            HomePhone = "+49 1522 3433336",
            Address = "Berlin"
        }, new Contact
        {
            ContactId = 2,
            Address = "Tehran",
            Name = "kourosh Honarkhah",
            HomePhone = "02177293029",
            MobilePhone = "+98 912 0724190",
            WorkPhone = "021 65800001",
            Email = "MyEmailAddress@gmail.com"
        }, new Contact
        {
            ContactId = 3,
            Address = "Tehran",
            Name = "Tanin Honarkhah",
            HomePhone = "+98 21 77711231",
            MobilePhone = "+98 912 0724191",
            WorkPhone = "02 165801001",
            Email = "second@gmail.com"
        }, new Contact
        {
            ContactId = 4,
            Address = "Berlin",
            Name = "jane Doe",
            HomePhone = "+49 33 8787564",
            MobilePhone = "+499120724191",
            WorkPhone = "02165801001",
            Email = "jane.doe@gmail.com"
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
    {
        optionbuilder.UseSqlite(@"Data Source=d:\Address-Book.db");
    }
}