using Core.Entities;
using Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class ContactRepository : IContactRepository
    , IDisposable
{
    private readonly SqliteDbContext _context;

    private bool disposed = false;

    public ContactRepository(SqliteDbContext context)
    {
        _context = context;
    }


    public async Task<List<Contact>> GetAllContacts()
    {
        return  await _context.Contacts.ToListAsync();
    }

    public List<Contact> GetAll()
    {
        return _context.Contacts.ToList();
    }

    public Task<Contact> GetContactById(long Id)
    {
        return _context.Contacts.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<Contact> AddContact(Contact contact)
    {
        
        var addedEntity = _context.Contacts.Add(contact);
        _context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public async Task<Contact> UpdateContact(Contact contact)
    {
        var foundEntity = _context.Contacts.FirstOrDefault(x => x.Id == contact.Id);

        if (foundEntity == null) return null;

        foundEntity.FirstName = contact.FirstName;
        foundEntity.LastName = contact.LastName;
        foundEntity.Address = contact.Address;
        foundEntity.Email = contact.Email;
        foundEntity.MobilePhone = contact.MobilePhone;
        foundEntity.HomePhone = contact.HomePhone;
        foundEntity.WorkPhone = contact.WorkPhone;

        _context.SaveChangesAsync();

        return foundEntity;
    }

    public async Task<Contact>  DeleteContact(long Id)
    {
        var foundEntity = _context.Contacts.FirstOrDefault(x => x.Id == Id);
        if (foundEntity == null)
            return null;

        _context.Contacts.Remove(foundEntity);
       await _context.SaveChangesAsync();
       return foundEntity;
    }
    public async Task<bool> GetContactByEmail(string email)
    {
        return await _context.Contacts
            .AnyAsync(e => e.Email == email);
    }

    private void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}