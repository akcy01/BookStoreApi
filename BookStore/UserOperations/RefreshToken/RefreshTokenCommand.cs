using AutoMapper;
using BookStore.Data;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.UserOperations.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly ApplicationDbContext _dbcontext;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _dbcontext = context;
        }
        public Token Handle()
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireData > DateTime.Now);
            if (user is not null)
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
}
