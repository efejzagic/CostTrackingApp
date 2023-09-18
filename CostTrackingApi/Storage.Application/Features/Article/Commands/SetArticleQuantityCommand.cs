using AutoMapper;
using MediatR;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Article.Commands
{
    public class SetArticleQuantityCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public class SetArticleQuantityHandler : IRequestHandler<SetArticleQuantityCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Article> _Repository;
            public SetArticleQuantityHandler(IGenericRepositoryAsync<Domain.Entities.Article> Repository)
            {
                _Repository = Repository;
            }

            public async Task<Response<string>> Handle(SetArticleQuantityCommand request, CancellationToken cancellationToken)
            {
                var article = await _Repository.GetByIdAsync(request.Id);
                article.Quantity += request.Quantity;
                await _Repository.UpdateAsync(article);
                return new Response<string>("Article quantity updated");
            }
        }
    }
}
