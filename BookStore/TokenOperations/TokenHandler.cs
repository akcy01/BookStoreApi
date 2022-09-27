using BookStore.Models;
using BookStore.TokenOperations.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookStore.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credientals = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            tokenModel.Expiration = DateTime.Now.AddMinutes(15);
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: Configuration["Token:Issuer"],// ile jetonun kim tarafından oluşturup imzalandığı
                audience:Configuration["Token:Audience"],
                expires:tokenModel.Expiration,
                notBefore:DateTime.Now,
                signingCredentials: credientals
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token is created.
            tokenModel.AccessToken = tokenHandler.WriteToken(token);
            tokenModel.RefreshToken = CreateRefreshToken();
            return tokenModel;
        }
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
