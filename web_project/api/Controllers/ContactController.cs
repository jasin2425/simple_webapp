using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Contact;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
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
    private readonly ICategoryRepository _categoryrepo;
    public ContactController(ApplicationDBContext context,IContactRepository repo, ICategoryRepository categoryRepo)
    {
        _repo=repo;
        _context = context;
        _categoryrepo=categoryRepo;
    }
    //get all Contacts
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllContacts()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var contacts = await _repo.GetAllasync();

            var minimalisticcontactdtoo =contacts.Select(s=>s.toMinimalisticContactDto());        
        
            return Ok(minimalisticcontactdtoo);
        }
        //get contact by id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);    

        var contact = await _repo.GetByIdAsync(id);
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact.toDetailContactDto());
        }

        //create contact
        [HttpPost("{categoryId:int}")]
public async Task<IActionResult> Create(CreateContactDTO contactDTO, [FromRoute] int categoryId)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    if (!await _categoryrepo.CategoryExists(categoryId))
        return BadRequest("Category does not exist");

    if (!string.IsNullOrWhiteSpace(contactDTO.CustomSubcategory))
    {
        var newSub = new Subcategory { Name = contactDTO.CustomSubcategory };
        await _context.Subcategories.AddAsync(newSub);
        await _context.SaveChangesAsync();

        // Dodaj powiązanie do CategoryandSubcategory
        var relation = new CategoryandSubcategory
        {
            CategoryId = categoryId,
            SubcategoryId = newSub.Id
        };
        await _context.CategorySubcategories.AddAsync(relation);
        await _context.SaveChangesAsync();

        contactDTO.SubcategoryId = newSub.Id;
    }

    var contactModel = contactDTO.ToContactCreateDTO(categoryId);
    await _repo.CreateAsync(contactModel);

    return CreatedAtAction(nameof(getById), new { id = contactModel.Id }, contactModel.ToContactDTO());
}


        //update contact
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactDTO contact){
           if(!ModelState.IsValid)
                return BadRequest(ModelState); 
    
            var contactModel=await _repo.UpdateAsync(id,contact.ToContactUpdateDTO());
            if(contactModel==null)
            {
                return NotFound("Contact not found");
            }
         
            return Ok(contactModel.ToContactDTO());

        }

        //deleting contact
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState); 

            var contactModel =await _repo.DeleteAsync(id);
            if(contactModel==null)
            {
                return NotFound("Contact not found");
            }
            return NoContent();
        }
    }
}
