
using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Article;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CorrelationIdLibrary.Interfaces;

namespace Storage.Application.Features.Article.Commands
{
    public partial class CreateArticleCommand : IRequest<Response<ArticleDTO>>
    {
        public ArticleCreateDTO Value { get; set; }

    }
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Response<ArticleDTO>>
    {

        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.Article> _Repository;
        private readonly IMapper _mapper;
        public CreateArticleCommandHandler(IGenericRepositoryAsync<Storage.Domain.Entities.Article> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<ArticleDTO>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Storage.Domain.Entities.Article>(request.Value);
            await _Repository.AddAsync(enviroment);
            var response = _mapper.Map< ArticleDTO > (enviroment);
            return new Response<ArticleDTO>(response);
        }
    }
}
