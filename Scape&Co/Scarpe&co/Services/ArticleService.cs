using Scarpe_co.Models;
using System.Collections.Generic;
using System.Linq;

namespace Scarpe_co.Services
{
    public class ArticleService : ICrudService<Article>
    {
        private static List<Article> articles = new List<Article>();

        public IEnumerable<Article> GetAll() => articles;

        public Article GetById(int id) => articles.FirstOrDefault(a => a.Id == id);

        public void Add(Article article)
        {
            article.Id = articles.Count > 0 ? articles.Max(a => a.Id) + 1 : 1;
            articles.Add(article);
        }

        public void Update(Article article)
        {
            var existing = GetById(article.Id);
            if (existing != null)
            {
                existing.Name = article.Name;
                existing.Price = article.Price;
                existing.Description = article.Description;
                existing.CoverImage = article.CoverImage;
                existing.AdditionalImage1 = article.AdditionalImage1;
                existing.AdditionalImage2 = article.AdditionalImage2;
            }
        }

        public void Delete(int id) => articles.RemoveAll(a => a.Id == id);
    }
}
