using ElevateOTT.Domain.Entities.POC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevateOTT.WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TestController : ApiController
{
    private readonly IApplicationDbContext _dbContext;

    public TestController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        //var response = await Mediator.Send(new CreateTenantCommand
        //{
        //    TenantName = "testerjane"
        //});

        //_dbContext.Applicants.Add(new Applicant
        //{
        //    Ssn=123456,
        //    FirstName = "tom",
        //    LastName = "jones",
        //    Height= 180,
        //    Weight = 8,
        //    TenantId = Guid.Parse("4bf31d54-ba4f-42b2-963c-fd3246bfdf6d")
        //});

        //await _dbContext.SaveChangesAsync();


        var users = await _dbContext.Users.ToListAsync();
        var applicants = await _dbContext.Applicants.ToListAsync();
        var tenants = await _dbContext.Tenants.ToListAsync();
        return Ok();
    }
}
