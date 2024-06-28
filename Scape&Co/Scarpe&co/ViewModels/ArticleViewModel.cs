using Microsoft.AspNetCore.Http;

namespace Scarpe_co.ViewModels
{
    public class ArticleViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile CoverImage { get; set; }
        public IFormFile AdditionalImage1 { get; set; }
        public IFormFile AdditionalImage2 { get; set; }
    }
}
