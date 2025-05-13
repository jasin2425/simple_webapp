using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SubcategoryRepository:ISubcategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public SubcategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
         public Task<bool> SubcategoryExists(int id)
        {
            return _context.Subcategories.AnyAsync(s=> s.Id==id);
        }

       public async Task<Subcategory?> GetByIdAsync(int id)
        {
            return await _context.Subcategories
                .Include(s => s.CategorySubcategories)
                    .ThenInclude(cs => cs.Category)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Subcategory>> GetAllasync()
        {
                return await _context.Subcategories.Include(c=>c.CategorySubcategories).ToListAsync();
        }


        public async Task<Subcategory> CreateAsync(Subcategory subcategoryModel)
        {
              await _context.Subcategories.AddAsync(subcategoryModel);
            await _context.SaveChangesAsync();
            return subcategoryModel;
        }
    }
}