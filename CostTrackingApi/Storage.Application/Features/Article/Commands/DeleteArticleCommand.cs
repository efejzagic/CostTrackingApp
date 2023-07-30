using AutoMapper;
using MediatR;
using Storage.Application.Interfaces;
using Storage.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Article.Commands
{
    public class DeleteArticleCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Article> _Repository;
            private readonly IMapper _mapper;
            public DeleteArticleCommandHandler(IGenericRepositoryAsync<Domain.Entities.Article> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Article>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
