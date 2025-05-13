using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDBContext context)
        {
            if (!context.Categories.Any())
            {
                var prywatny = new Category { Name = "prywatny" };
                var sluzbowy = new Category { Name = "służbowy" };
                var inny = new Category { Name = "inny" };

                context.Categories.AddRange(prywatny, sluzbowy, inny);
                context.SaveChanges();

                // subkategorie tylko dla służbowego
                var sub1 = new Subcategory { Name = "IT" };
                var sub2 = new Subcategory { Name = "HR" };
                var sub3 = new Subcategory { Name = "Marketing" };

                context.Subcategories.AddRange(sub1, sub2, sub3);
                context.SaveChanges();

                var relacje = new List<CategoryandSubcategory>
                {
                    new() { CategoryId = sluzbowy.Id, SubcategoryId = sub1.Id },
                    new() { CategoryId = sluzbowy.Id, SubcategoryId = sub2.Id },
                    new() { CategoryId = sluzbowy.Id, SubcategoryId = sub3.Id },
                };

                context.CategorySubcategories.AddRange(relacje);
                context.SaveChanges();
            }
        }
    }
}