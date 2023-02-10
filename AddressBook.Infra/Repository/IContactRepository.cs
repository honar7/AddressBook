using Core.Entities;

namespace Infra.Repository;

public interface IContactRepository
    // : IDisposable
{
    Task<IEnumerable<Contact>> GetAllContacts();

    Contact GetContactById(int contactId);

    Contact AddContact(Contact contact);

    Contact UpdateContact(Contact contact);

    void DeleteContact(int contactId);
}