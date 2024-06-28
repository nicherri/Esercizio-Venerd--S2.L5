namespace ScarpeCo.Entities
{
    public class Article : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string AdditionalImage1 { get; set; }
        public string AdditionalImage2 { get; set; }
    }
}
