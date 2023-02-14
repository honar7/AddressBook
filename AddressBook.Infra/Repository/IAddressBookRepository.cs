using Core.Entities;

namespace Infra.Repository;

public interface IAddressBookService
{
    Task<IEnumerable<Contact>> GetContactAsync();
}