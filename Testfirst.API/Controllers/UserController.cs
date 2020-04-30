using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testfirst.API.Data;
using Testfirst.API.Dtos;

namespace Testfirst.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]/{action}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn=_mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int Id)
        {
            var user = await _repo.GetUser(Id);
            var userToReturn=_mapper.Map<UserForDetailDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserForUpdateDto userForUpdate){
            if(userForUpdate.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userForUpdate.Id);

            _mapper.Map(userForUpdate, userFromRepo);
            if(await _repo.SaveAll())
                return NoContent();
            throw new System.Exception($"Updating user {userForUpdate.Id} failed on save!");
        }
    }
}