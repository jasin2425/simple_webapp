using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

[Route("api/[controller]")]
[ApiController]
public class SubcategoryController:ControllerBase
{
    private readonly ApplicationDBContext _context;
    public SubcategoryController(ApplicationDBContext context)
    {
        _context=context;
    }
    //Get: Subcategories
    [HttpGet]
    public IActionResult GetAllSubcategories()
    {
        var subcategories = _context.Subcategories.ToList();        
        return Ok(subcategories);
    }
    [HttpGet("{id}")]

    public IActionResult getById([FromRoute] int id)
    {
        var subcategory=_context.Subcategories.Find(id);
        if (subcategory == null)
        {
            return NotFound();
        }
        return Ok(subcategory);
    }
     
    }
}