using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Contact;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IContactRepository _repo;
    public ContactController(ApplicationDBContext context,IContactRepository repo)
    {
        _repo=repo;
        _context = context;
    }
    //Get: Contacts
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _repo.GetAllasync();

            var contactdtoo =contacts.Select(s=>s.ToContactDTO());        
        
            return Ok(contactdtoo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
        var contact = await _repo.GetByIdAsync(id);
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
        }
         [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDTO contactDTO)
        {
            var contactModel= contactDTO.ToContactCreateDTO();
            await _repo.CreateAsync(contactModel);
            return CreatedAtAction(nameof(getById),new{id=contactModel.Id},contactModel.ToContactDTO());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContact contactDTO){
            var contactModel=await _repo.UpdateAsync(id,contactDTO);
            if(contactModel==null)
            {
                return NotFound();
            }
         
            return Ok(contactModel.ToContactDTO());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var contactModel =await _repo.DeleteAsync(id);
            if(contactModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
