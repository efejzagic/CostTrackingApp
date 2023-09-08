using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Article;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Article.Commands
{
    public class UpdateArticleCommand : IRequest<Response<string>>
    {
        //public int Id { get; set; }
        public ArticleEditDTO Value { get; set; }
        public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Article> _Repository;
            private readonly IMapper _mapper;
            public UpdateArticleCommandHandler(IGenericRepositoryAsync<Domain.Entities.Article> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Article>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
