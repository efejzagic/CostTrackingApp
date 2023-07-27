
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.Wrappers;
using Storage.Application.Interfaces;
using Storage.Application.DTOs.Article;

namespace Storage.Application.Features.Article.Queries
{
    public class GetArticleByNameQuery : IRequest<Response<ArticleDTO>>
    {
        public string Name { get; set; }
    }
    public class GetArticleByNameQueryHandler : IRequestHandler<GetArticleByNameQuery, Response<ArticleDTO>>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;
        public GetArticleByNameQueryHandler(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ArticleDTO>> Handle(GetArticleByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ArticleDTO>(request);
            var enviroment = await _repository.GetByName(request.Name);
            if (enviroment == null)
            {
                return new Response<ArticleDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with name = {request.Name}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ArticleDTO>(enviroment);
            return new Response<ArticleDTO>(enviromentViewModel);
        }
    }
}
