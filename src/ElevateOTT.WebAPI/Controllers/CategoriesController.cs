using AutoWrapper.Filters;
using ElevateOTT.Application.Features.Content.Categories.Commands.CreateCategory;
using ElevateOTT.Application.Features.Content.Categories.Commands.DeleteCategory;
using ElevateOTT.Application.Features.Content.Categories.Commands.UpdateCategory;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategoryForEdit;

namespace ElevateOTT.WebAPI.Controllers;

//[BpCategoryize]
[AllowAnonymous]
[Route("api/categories")]
[ApiController]
public class CategoriesController : ApiController
{
    #region Public Methods

    [HttpGet("{id:guid}", Name = "CategoryById")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(new GetCategoryForEditQuery { Id = id });
        return TryGetResult(response);
    }

    //[AutoWrapIgnore]
    [HttpPost]
    public async Task<IActionResult> GetCategories([FromBody] GetCategoriesQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);

        return TryGetResult(response);
    }

    [HttpPost("auto-complete")]
    public async Task<IActionResult> GetCategoriesForAutoComplete([FromBody] GetCategoriesForAutoCompleteQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);

        return TryGetResult(response);
    }

    [HttpPost("add")]
    [RequestFormLimits(MultipartBodyLengthLimit = 20971520)] // 20MB
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var response = await Mediator.Send(new DeleteCategoryCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods
}
