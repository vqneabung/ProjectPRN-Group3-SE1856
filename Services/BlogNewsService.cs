using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BlogNewsService : IService<BlogNews>
    {
        private readonly IRepository<BlogNews> _repository;

        public BlogNewsService(IRepository<BlogNews> repository)
        {
            _repository = repository;
        }

        public void Add(BlogNews entity)
        {
            _repository.Add(entity);
        }

        public void Delete(BlogNews entity)
        {
            _repository.Delete(entity);
        }

        public BlogNews? Get(int id)
        {
            var blogNews = _repository.Get(id);
            return blogNews;
        }

        public IEnumerable<BlogNews>? GetAll()
        {
            var blogNews = _repository.GetAll();
            return blogNews;
        }

        public void Update(BlogNews entity)
        {
            _repository.Update(entity);
        }
    }
}
