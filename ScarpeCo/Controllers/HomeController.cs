using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ScarpeCo.Entities;
using ScarpeCo.Models;
using ScarpeCo.Services;
using System.IO;
using System.Linq;

namespace ScarpeCo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IWebHostEnvironment _env;

        public HomeController(IArticleService articleService, IWebHostEnvironment env)
        {
            _articleService = articleService;
            _env = env;
        }

        public IActionResult Index()
        {
            var articles = _articleService.GetAll()
                .Select(a => new ArticleViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Description = a.Description,
                    CoverImage = a.CoverImage,
                    AdditionalImage1 = a.AdditionalImage1,
                    AdditionalImage2 = a.AdditionalImage2
                }).ToList();
            return View(articles);
        }

        public IActionResult Details(int id)
        {
            var article = _articleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }

            var model = new ArticleViewModel
            {
                Id = article.Id,
                Name = article.Name,
                Price = article.Price,
                Description = article.Description,
                CoverImage = article.CoverImage,
                AdditionalImage1 = article.AdditionalImage1,
                AdditionalImage2 = article.AdditionalImage2
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new Article
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description
                };

                _articleService.Add(article);

                string uploads = Path.Combine(_env.WebRootPath, "images");

                if (model.CoverImageFile != null && model.CoverImageFile.Length > 0)
                {
                    string filePath = Path.Combine(uploads, $"{article.Id}_cover.jpg");
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.CoverImageFile.CopyTo(fileStream);
                    }
                    article.CoverImage = $"/images/{article.Id}_cover.jpg";
                }

                if (model.AdditionalImage1File != null && model.AdditionalImage1File.Length > 0)
                {
                    string filePath = Path.Combine(uploads, $"{article.Id}_additional1.jpg");
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.AdditionalImage1File.CopyTo(fileStream);
                    }
                    article.AdditionalImage1 = $"/images/{article.Id}_additional1.jpg";
                }

                if (model.AdditionalImage2File != null && model.AdditionalImage2File.Length > 0)
                {
                    string filePath = Path.Combine(uploads, $"{article.Id}_additional2.jpg");
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.AdditionalImage2File.CopyTo(fileStream);
                    }
                    article.AdditionalImage2 = $"/images/{article.Id}_additional2.jpg";
                }

                _articleService.Update(article);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
