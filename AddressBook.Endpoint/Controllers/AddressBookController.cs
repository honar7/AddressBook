using Core.Entities;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressBookController : Controller
{
    private readonly IContactRepository _contactRepository;

    public AddressBookController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpGet]
    [Route("GetAllContacts")]
    public  List<Contact> GetAllContacts()
    {
        return  _contactRepository.GetAllContacts();
    }
}