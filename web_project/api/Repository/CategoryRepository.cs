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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<bool> CategoryExists(int id)
        {
            return _context.Categories.AnyAsync(s=> s.Id==id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
                return await _context.Categories.Include(c=>c.Contacts).ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.Include(c=>c.Contacts).FirstOrDefaultAsync(i => i.Id ==id);
        }
    }
}