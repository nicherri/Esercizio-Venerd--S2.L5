using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ScarpeCo.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Il prezzo deve essere compreso tra 0,01 e 10.000,00 euro.")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public string CoverImage { get; set; }
        public string AdditionalImage1 { get; set; }
        public string AdditionalImage2 { get; set; }

        [Display(Name = "Immagine di copertina")]
        public IFormFile CoverImageFile { get; set; }

        [Display(Name = "Immagine aggiuntiva 1")]
        public IFormFile AdditionalImage1File { get; set; }

        [Display(Name = "Immagine aggiuntiva 2")]
        public IFormFile AdditionalImage2File { get; set; }
    }
}
