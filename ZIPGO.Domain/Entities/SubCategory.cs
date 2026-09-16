using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
   public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MainCategoryId { get; set; }

        public MainCategory MainCategory { get; set; }

        public ICollection<Product> Products { get; set; }

    }


}
