using Core.Entities;

namespace Infra.Repository;

public interface IContactRepository
    // : IDisposable
{
    List<Contact> GetAllContacts();

    Contact GetContactById(long Id);

    Contact AddContact(Contact contact);

    Contact UpdateContact(Contact contact);

    void DeleteContact(long Id);
}