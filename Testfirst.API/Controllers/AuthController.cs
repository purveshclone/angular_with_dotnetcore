using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Testfirst.API.Data;
using Testfirst.API.Dtos;
using Testfirst.API.Models;

namespace Testfirst.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto UserForRegister)
        {
            UserForRegister.Username = UserForRegister.Username.ToLower();
            if (await _repo.UserExist(UserForRegister.Username))
                return BadRequest("username already exist !!");

            // var userToCreate = new Users
            // {
            //     UserName = UserForRegister.Username
            // };
            var userToCreate = _mapper.Map<Users>(UserForRegister);
            var createdUser = await _repo.Register(userToCreate, UserForRegister.Password);
            // return StatusCode(201);
            var userToReturn = _mapper.Map<UserForDetailDto>(createdUser);
            return CreatedAtRoute("GetUser", new { Controller = "user", id = createdUser.Id, Action="GetUser" }, userToReturn);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLOginDto userForLogin)
        {

            // throw new System.Exception("Computer says no!!");

            var userFromRepo = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name , userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var uswer = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token),
                user = uswer
            });
        }

    }
}