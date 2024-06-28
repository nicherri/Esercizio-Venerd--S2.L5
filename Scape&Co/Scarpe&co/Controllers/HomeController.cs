using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scarpe_co.Models;
using Scarpe_co.Services;
using Scarpe_co.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Scarpe_co.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICrudService<Article> _articleService;

        public HomeController(ICrudService<Article> articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            var articles = _articleService.GetAll();
            return View(articles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new Article
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CoverImage = await SaveImage(model.CoverImage),
                    AdditionalImage1 = await SaveImage(model.AdditionalImage1),
                    AdditionalImage2 = await SaveImage(model.AdditionalImage2)
                };

                _articleService.Add(article);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var article = _articleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            if (image != null)
            {
                var filePath = Path.Combine("wwwroot/images", Path.GetFileName(image.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                return $"/images/{Path.GetFileName(image.FileName)}";
            }
            return null;
        }
    }
}
