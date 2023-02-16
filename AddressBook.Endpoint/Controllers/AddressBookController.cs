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


    [HttpGet("{id:long}")]
    public Task<Contact> GetContactById(long id)
    {
        return _contactRepository.GetContactById(id);
    }

    [HttpGet]
    [Route("GetAllContact")]
    public async Task<List<Contact>> GetAllContact()
    {
        return await _contactRepository.GetAllContacts();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllContacts()
    {
        try
        {
            return Ok(await _contactRepository.GetAllContacts());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContact(Contact contact)
    {
        try
        {
            if (contact == null) return BadRequest();

            var con = _contactRepository.GetContactByEmail(contact.Email);

            if (con.Result)
            {
                ModelState.AddModelError("email", "Contact email already in use");
                return BadRequest(ModelState);
            }

            var createdContact = await _contactRepository.AddContact(contact);

            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id },
                createdContact);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Contact>> UpdateContact(int id, Contact contact)
    {
        try
        {
            if (id != contact.Id)
                return BadRequest("Contact ID mismatch");

            var contactToUpdate = await _contactRepository.GetContactById(id);

            if (contactToUpdate == null)
                return NotFound($"Contact with Id = {id} not found");

            return await _contactRepository.UpdateContact(contact);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Contact>> DeleteContact(int id)
    {
        try
        {
            var contactToDelete = await _contactRepository.GetContactById(id);

            return contactToDelete == null
                ? NotFound($"Contact with Id = {id} not found")
                : await _contactRepository.DeleteContact(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
}