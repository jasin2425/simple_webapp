using api.Data;
using api.DTO;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Interfaces;
using System.Threading.Tasks;
namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
    private readonly ApplicationDBContext _context;
    private readonly ICategoryRepository _repo;
   public CategoryController(ICategoryRepository repository)
   {
      _repo=repository;
   }
   
   [HttpGet]
   public async Task<IActionResult> GetAllCategories()
   {
          if(!ModelState.IsValid)
                return BadRequest(ModelState);
      var categories = await _repo.GetAllAsync();
      var categories_dto= categories.Select( s => s.ToCategoryDTO());
      return Ok(categories_dto);
   }
   [HttpGet("{id:int}")]


   public async Task<IActionResult> getById([FromRoute] int id)
   {
          if(!ModelState.IsValid)
                return BadRequest(ModelState);
      var category= await _repo.GetById(id);
      if (category == null)
         return NotFound();
      return Ok(category.ToCategoryDTO());
   }
  
 }
}
