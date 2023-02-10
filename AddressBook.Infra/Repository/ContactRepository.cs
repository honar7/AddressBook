using Core.Entities;
using Infra.Context;

namespace Infra.Repository;

public class ContactRepository : IContactRepository
    // , IDisposable
{
    private readonly SqliteDbContext _context;

    private bool disposed = false;

    public ContactRepository(SqliteDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Contact>> GetAllContacts()
    {
        return _context.Contacts;
    }

    public Contact GetContactById(int contactId)
    {
        return _context.Contacts.FirstOrDefault(x => x.ContactId == contactId);
    }

    public Contact AddContact(Contact contact)
    {
        var addedEntity = _context.Contacts.Add(contact);
        _context.SaveChanges();
        return addedEntity.Entity;
    }

    public Contact UpdateContact(Contact contact)
    {
        var foundEntity = _context.Contacts.FirstOrDefault(x => x.ContactId == contact.ContactId);

        if (foundEntity == null) return null;

        foundEntity.Name = contact.Name;
        foundEntity.Address = contact.Address;
        foundEntity.Email = contact.Email;
        foundEntity.MobilePhone = contact.MobilePhone;
        foundEntity.HomePhone = contact.HomePhone;
        foundEntity.WorkPhone = contact.WorkPhone;

        _context.SaveChanges();

        return foundEntity;
    }

    public void DeleteContact(int contactId)
    {
        var foundEntity = _context.Contacts.FirstOrDefault(x => x.ContactId == contactId);
        if (foundEntity == null)
            return;

        _context.Contacts.Remove(foundEntity);
        _context.SaveChanges();
    }


    // private void Dispose(bool disposing)
    // {
    //     if (!this.disposed)
    //     {
    //         if (disposing)
    //         {
    //             _context.Dispose();
    //         }
    //     }
    //     this.disposed = true;
    // }
    //
    // public void Dispose()
    // {
    //     Dispose(true);
    //     GC.SuppressFinalize(this);
    // }
}