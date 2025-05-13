using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Subcategory;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

[Route("api/[controller]")]
[ApiController]
public class SubcategoryController:ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly ISubcategoryRepository _repo;
    public SubcategoryController(ISubcategoryRepository repo)
    {
        _repo=repo;
    }
    //Get: Subcategories
    [HttpGet]
    public async Task<IActionResult> GetAllSubcategories()
    {
        if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var subcategories = await _repo.GetAllasync();
        var subcategories_dto =subcategories.Select(s=>s.ToSubcategoryDto());       
        return Ok(subcategories_dto);
    }
    [HttpGet("{id}")]

    public async Task<IActionResult> getById([FromRoute] int id)
    {
         if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var subcategory=await _repo.GetByIdAsync(id);
        if (subcategory == null)
        {
            return NotFound();
        }
        return Ok(subcategory.ToSubcategoryDto());
    }
     [HttpPost("{categoryId:int}")]
public async Task<IActionResult> Create([FromBody] SubcategoryCreate dto, [FromRoute] int categoryId)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var category = await _context.Categories.FindAsync(categoryId);
    if (category == null)
        return BadRequest("Category does not exist");

    // utwórz subkategorię
    var model = dto.ToSubcategoryFromCreate();
    await _context.Subcategories.AddAsync(model);
    await _context.SaveChangesAsync();

    // powiąż w tabeli pośredniej
    var relation = new CategoryandSubcategory
    {
        CategoryId = categoryId,
        SubcategoryId = model.Id
    };
    await _context.CategorySubcategories.AddAsync(relation);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(getById), new { id = model.Id }, model);
}

    }
}