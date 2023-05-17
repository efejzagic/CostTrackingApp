using AutoMapper;
using StorageService.DTO;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Services
{
    public class ArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepo;
        public ArticleService(IMapper mapper, IArticleRepository articleRepo)
        {
            _mapper = mapper;
            _articleRepo = articleRepo;
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticles ()
        {
            var articles = await _articleRepo.GetArticles();
            return _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(articles);

        }
    }
}
