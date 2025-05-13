using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Subcategory
{
    public class SubcategoryDTO
    {
         [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}