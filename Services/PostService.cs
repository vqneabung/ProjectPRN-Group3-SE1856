using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  class PostService : IService<Post>  
    {
        private readonly IRepository<Post> _postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public void Add(Post entity)
        {
             _postRepository.Add(entity);
        }

        public void Delete(Post entity)
        {
             _postRepository.Delete(entity);
        }

        public Post? Get(int id)
        {
            return _postRepository.GetByID(id);
        }

        public IEnumerable<Post>? GetAll()
        {
           return _postRepository.GetAll();
        }

        public void Update(Post entity)
        {
            _postRepository.Update(entity);
        }
    }
}
