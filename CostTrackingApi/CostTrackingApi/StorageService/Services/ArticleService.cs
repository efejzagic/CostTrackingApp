using AutoMapper;
using StorageService.DTO.Article;
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
        public async Task<IEnumerable<ArticleDTO>> GetArticlesBySupplierId(int supplierId)
        {
            var articles = await _articleRepo.GetArticlesBySupplierId(supplierId);
            return _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(articles);

        }

        public async Task<ArticleDTO> GetById(int id)
        {
            var article = await _articleRepo.GetById(id);
            return _mapper.Map<Article, ArticleDTO>(article);
        }

        public async Task<ArticleDTO> Create(ArticleCreateDTO article)
        {
            var mapped = _mapper.Map<Article>(article);
            var newArticle = await _articleRepo.Create(mapped);
            return _mapper.Map<Article, ArticleDTO>(newArticle);
        }

        public async Task<ArticleDTO> Edit(ArticleEditDTO article)
        {
            var mapped = _mapper.Map<Article>(article);
            mapped.DateModified = DateTime.UtcNow;
            var newArticle = await _articleRepo.Edit(mapped);
            return _mapper.Map<Article, ArticleDTO>(newArticle);
        }

        public async Task<bool> Delete(int id)
        {
            return await _articleRepo.Delete(id);
        }

        public async Task<bool> SoftDelete(int id)
        {
            return await _articleRepo.SoftDelete(id);
        }

    }
}
