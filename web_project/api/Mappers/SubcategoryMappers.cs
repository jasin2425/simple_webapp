using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Subcategory;
using api.Models;

namespace api.Mappers
{
    public static class SubcategoryMappers
    {
        public static SubcategoryDTO ToSubcategoryDto(this Subcategory subcategory)
        {
            return new SubcategoryDTO{
                Id=subcategory.Id,
                Name=subcategory.Name
            };
        }
        public static Subcategory ToSubcategoryFromCreate(this SubcategoryCreate subcreate)
        {
            return new Subcategory{
                Name=subcreate.Name
            };
        }
    }
}