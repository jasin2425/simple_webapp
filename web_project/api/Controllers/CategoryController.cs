using api.Data;
using api.DTO;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
    private readonly ApplicationDBContext _context;
   public CategoryController(ApplicationDBContext context)
   {
      _context=context;
   }
   
   [HttpGet]
   public IActionResult GetAllCategories()
   {
      var categories = _context.Categories.ToList()
         .Select( s => s.ToCategoryDTO());
            return Ok(categories);
   }
   [HttpGet("{id}")]


   public IActionResult getById([FromRoute] int id)
   {
      var category=_context.Categories.Find(id);
      if (category == null)
         return NotFound();
      return Ok(category.ToCategoryDTO());
   }
  
 }
}
