using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Features.Article.Commands;
using Storage.Application.Features.Article.Queries;
using Storage.Application.Parameters.Article;
using WebApi.Controllers;

namespace Storage.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    
    public class ArticleController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [Authorize(Roles = "Storage Manager,Finance")]

        public async Task<IActionResult> Get([FromQuery] GetAllArticleParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllArticleQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Storage Manager,Finance")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetArticleByIdQuery { Id = id }));
        }

        [HttpGet("Name/{Name}")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetArticleByNameQuery { Name = name }));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Post(CreateArticleCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            //var enviroment = await Mediator.Send(command);
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
       
        }




        [HttpPut]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Put(UpdateArticleCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteArticleCommand { Id = id });
            return Ok(enviroment);
        }


        [HttpPut("updateQuantity")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> UpdateArticleQuantity(SetArticleQuantityCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }


    }
}
