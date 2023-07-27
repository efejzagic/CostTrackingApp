using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.DTOs.Article;
using AutoMapper;
using Storage.Application.Interfaces;
using Storage.Application.Wrappers;

namespace Storage.Application.Features.Article.Queries
{
    public class GetArticleByIdQuery : IRequest<Response<ArticleDTO>>
    {
        public int Id { get; set; }
    }
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Response<ArticleDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Article> _repository;
        private readonly IMapper _mapper;
        public GetArticleByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.Article> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ArticleDTO>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ArticleDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<ArticleDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ArticleDTO>(enviroment);
            return new Response<ArticleDTO>(enviromentViewModel);
        }
    }
}
