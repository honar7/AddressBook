using Core.Entities;

namespace Infra.Repository;

public interface IAddressBookRepository
{
    Task<IEnumerable<Contact>> GetContactAsync();
}