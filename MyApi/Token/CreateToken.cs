using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyApi.Token
{
    public class CreateToken
    {
        private readonly IConfiguration _configuration;

        public CreateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string CreateTokenHandler(TokenRequestModel tokenRequest)
        {
            //Claim türünde liste oluşturup token içine claimlemek istenen dataları veriyoruz
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(tokenRequest.Id)),
                new Claim(ClaimTypes.Name, tokenRequest.UserName),
                new Claim(ClaimTypes.GivenName, tokenRequest.FirstName),
                new Claim(ClaimTypes.Surname, tokenRequest.LastName)
            };

            // token'ın imzalanmasında kullanılacak olan simetrik şifreleme anahtarı
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration["AppSettings:Token"]));

            // token için gerekli olan kimlik bilgilerini oluşturuyo
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //JwtSecurityToken sınıfından bir token oluşturuyoruz
            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings: Issuer"], // token'ı oluşturan yetkiliyi tanımlıyor
                audience: _configuration["AppSettings: Audience"], // token hedef kitlesi
                claims: claims, // tanımladığım kimlik bilgilerini token'a ekliyor
                expires: DateTime.Now.AddMinutes(300), // token'ın geçerlilik süresi
                signingCredentials: cred); // token'ı imzalamak için kullanılan kimlik bilgilerini belirtiyo

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
