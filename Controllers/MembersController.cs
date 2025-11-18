using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers //API controller returns API response (Default format = JSON)
{
   [Authorize]
    public class MembersController(IMemberRepository memberRepository) : BaseApiController  //We inject AppDb Context: When a request is received it routes it to the correct controller
    //This works since we have AbbDbContext registered in Program classic
    { //We need endpoints (See below)
        [HttpGet] //Http Request comes in, directs it to the controller on the url, if it's a get method, we create and return a list of users
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers() //Returns Http Responses
        { 
            return Ok (await memberRepository.GetMembersAsync());
        }

        [HttpGet("{id}")] // localhost:5001/api/members/bob-id - id acts as a route member
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id); //Only works with primary values
            if (member == null) return NotFound(); //404 error

            return member; 
        }

        [HttpGet("{id}/photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhotos(string id)
        {
            return Ok( await memberRepository.GetPhotosForMemberAsync(id));
        }
    } //to run you can use "dotnet watch" - not always reliable
}
