using AutoMapper;
using BookStore.Data;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.UserOperations.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IMapper mapper, IConfiguration configuration, ApplicationDbContext context)
        {
            _mapper = mapper;
            _configuration = configuration;
            _dbcontext = context;
        }
        public Token Handle()
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireData = token.Expiration.AddMinutes(5);
                _dbcontext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı Adı-Şifre hatalı.");
            }
        }
    }
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
