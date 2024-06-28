using ScarpeCo.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ScarpeCo.Services
{
    public class ArticleService : CrudService<Article>, IArticleService
    {
        private static List<Article> _articles = new List<Article>();

        public override void Add(Article entity)
        {
            entity.Id = _articles.Count > 0 ? _articles.Max(a => a.Id) + 1 : 1;
            _articles.Add(entity);
        }

        public override IEnumerable<Article> GetAll()
        {
            return _articles;
        }

        public override Article GetById(int id)
        {
            return _articles.FirstOrDefault(a => a.Id == id);
        }

        public override void Update(Article entity)
        {
            var article = GetById(entity.Id);
            if (article != null)
            {
                article.Name = entity.Name;
                article.Price = entity.Price;
                article.Description = entity.Description;
                article.CoverImage = entity.CoverImage;
                article.AdditionalImage1 = entity.AdditionalImage1;
                article.AdditionalImage2 = entity.AdditionalImage2;
            }
        }

        public override void Delete(int id)
        {
            var article = GetById(id);
            if (article != null)
            {
                _articles.Remove(article);
            }
        }
    }
}
