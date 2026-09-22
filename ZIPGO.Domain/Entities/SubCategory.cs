using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<MainCategory> MainCategories { get; set; }
            = new List<MainCategory>();

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}