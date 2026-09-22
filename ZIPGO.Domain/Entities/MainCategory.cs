using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class MainCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<SubCategory> SubCategories { get; set; }
            = new List<SubCategory>();
    }
}