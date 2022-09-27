using AutoMapper;
using BookStore.Data;
using BookStore.TokenOperations.Models;
using BookStore.UserOperations.CreateToken;
using BookStore.UserOperations.CreateUser;
using BookStore.UserOperations.RefreshToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration; //Bu appsettings'e ulaşmamızı sağlar.
        public UserController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Create(CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model = newUser;
            command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken(CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_mapper,_configuration,_context);
            command.Model = login;
            var token = command.Handle();
            return token;
        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken(string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_configuration,_context);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }
    }
}
