using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<List<Subcategory>> GetAllasync();
        Task<Subcategory?> GetByIdAsync(int id);
        Task<Subcategory> CreateAsync(Subcategory subcategoryModel);
    }
}