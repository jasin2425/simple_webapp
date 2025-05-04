using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Contact;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public ContactController(ApplicationDBContext context)
    {
        _context = context;
    }
    //Get: Contacts
        [HttpGet]
        public IActionResult GetAllContacts()
        {
        var contacts = _context.Contacts.ToList();        
        return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {
        var contact = _context.Contacts.Find(id);
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
        }
         [HttpPost]
        public IActionResult Create([FromBody] CreateContactDTO contactDTO)
        {
            var contactModel=contactDTO.ToContactCreateDTO();
            _context.Contacts.Add(contactModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById),new{id=contactModel.Id},contactModel.ToContactDTO());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateContact contactDTO){
            var contactModel=_context.Contacts.FirstOrDefault(x=>x.Id==id);
            if(contactModel==null)
            {
                return NotFound();
            }
            contactModel.BirthDate=contactDTO.BirthDate;
            contactModel.CategoryId=contactDTO.CategoryId;
            contactModel.Email=contactDTO.Email;
            contactModel.FirstName=contactDTO.FirstName;
            contactModel.LastName=contactDTO.LastName;
            contactModel.Phone=contactDTO.Phone;
            contactDTO.SubcategoryId=contactDTO.SubcategoryId;
            _context.SaveChanges();
            return Ok(contactModel.ToContactDTO());

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var contactModel =_context.Contacts.FirstOrDefault(x=>x.Id==id);
            if(contactModel==null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(contactModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
