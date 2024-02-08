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
    public class UpdateArticleCommand : IRequest<Response<ArticleDTO>>
    {
        //public int Id { get; set; }
        public ArticleEditDTO Value { get; set; }
        public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Response<ArticleDTO>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Article> _Repository;
            private readonly IMapper _mapper;
            public UpdateArticleCommandHandler(IGenericRepositoryAsync<Domain.Entities.Article> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<ArticleDTO>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Article>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                var response = _mapper.Map< ArticleDTO > (enviroment);
                return new Response<ArticleDTO>(response);
            }
        }
    }
}
