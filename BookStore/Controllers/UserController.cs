using AutoMapper;
using BookStore.Data;
using BookStore.UserOperations.CreateUser;
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
    }
}
