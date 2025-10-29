using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers //API controller returns API response (Default format = JSON)
{
    [Route("api/[controller]")] // localhost:5001/api/members - important to spell controller correctly for the name
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase  //We inject AppDb Context: When a request is received it routes it to the correct controller
    //This works since we have AbbDbContext registered in Program classic
    { //We need endpoints (See below)
        [HttpGet] //Http Request comes in, directs it to the controller on the url, if it's a get method, we create and return a list of users
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers() //Returns Http Responses
        {
            var members = await context.Users.ToListAsync(); //Async means that instead of requests being blocked while we are waiting, the action is delegated to another thread
            return members;
        }
        [HttpGet("{id}")] // localhost:5001/api/members/bob-id - id acts as a route member
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id); //Only works with primary values
            if (member == null) return NotFound(); //404 error

            return member; 
        }
    } //to run you can use "dotnet watch" - not always reliable
}
