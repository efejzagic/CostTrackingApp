using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.Wrappers;
using Storage.Application.DTOs.Article;
using Storage.Application.Interfaces;

namespace Equipment.Application.Features.Machinery.Queries
{
    public class GetAllArticleQuery : IRequest<PagedResponse<IEnumerable<ArticleDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        //public List<MachineryServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllArticleQueryHandler : IRequestHandler<GetAllArticleQuery, PagedResponse<IEnumerable<ArticleDTO>>>
    {
        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.Article> _repository;
        private readonly IMapper _mapper;
        public GetAllArticleQueryHandler(IGenericRepositoryAsync<Storage.Domain.Entities.Article> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ArticleDTO>>> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ArticleDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<ArticleDTO>>(enviroments);
            return new PagedResponse<IEnumerable<ArticleDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}