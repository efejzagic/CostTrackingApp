
using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Article;
using Storage.Application.Interfaces;
using Storage.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Application.Features.Article.Commands
{
    public partial class CreateArticleCommand : IRequest<Response<string>>
    {
        public ArticleCreateDTO Value { get; set; }

    }
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.Article> _Repository;
        private readonly IMapper _mapper;
        public CreateArticleCommandHandler(IGenericRepositoryAsync<Storage.Domain.Entities.Article> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Storage.Domain.Entities.Article>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
