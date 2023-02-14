using Core.Entities;

namespace Infra.Repository;

public class AddressBookService : IAddressBookService
{
    private readonly HttpClient _httpClient;
    private IEnumerable<Contact>? _contacts;

    public AddressBookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Contact>> GetContactAsync()
    {
        _contacts ??= await _httpClient.GetFromJsonAsync<Contact[]>("GetAllContacts" );
        return _contacts!;
    }
}