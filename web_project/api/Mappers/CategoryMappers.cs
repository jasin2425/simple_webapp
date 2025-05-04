using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDTO ToCategoryDTO(this Category categorymodel)
        {
            return new CategoryDTO
            {
                Id=categorymodel.Id,
                Name=categorymodel.Name
            };
        }
    }
}