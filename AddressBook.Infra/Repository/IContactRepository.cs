using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Repository;

public interface IContactRepository : IDisposable
{
    Task<Contact> AddContact(Contact contact);
    Task<List<Contact>> GetAllContacts();
    List<Contact> GetAll();
    Task<Contact> GetContactById(long Id);
    Task<Contact> UpdateContact(Contact contact);
    Task<Contact> DeleteContact(long Id);
    Task<bool> GetContactByEmail(string email);
}