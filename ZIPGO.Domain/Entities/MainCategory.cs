namespace ZIPGO.Domain.Entities
{
    public class MainCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // One MainCategory can have many SubCategories
        public ICollection<SubCategory> SubCategories { get; set; }
            = new List<SubCategory>();
    }
}