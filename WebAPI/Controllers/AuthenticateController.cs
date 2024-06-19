using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Application;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Authorization;
using MimeKit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MimeKit.Text;
using Microsoft.Net.Http.Headers;
namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase  
    {
        private readonly IAccountService _userService;
        public IConfiguration _configuration;
        public AuthenticateController(IAccountService userService, IConfiguration configuration) 
        {
            _userService = userService; 
            _configuration = configuration; 
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginAsync([FromBody] AccountDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest();
            }

            var user = await _userService.FindAccountAsync(loginDTO);

            string hashedPassword = ComputeSha256Hash(loginDTO.Password);   

            if (user != null && user.Password.CompareTo(hashedPassword) == 0 )
            {
                
                var issuer = _configuration["Jwt:Issuer"].ToString();
                var audience = _configuration["Jwt:Audience"].ToString();
                var key = Encoding.ASCII.GetBytes
                (_configuration["Jwt:Key"].ToString());
                _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("CompanyId", user.CompanyId.ToString()),
                        new Claim("AccountId", user.AccountId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()
                ),
                        new Claim(ClaimTypes.Role, user.Role),
                    }),
                    
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                var refreshToken = GenerateRefreshToken();
                // Lưu AccessToken vào Cookies
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                };
                Response.Cookies.Append("accessToken", stringToken, cookieOptions);
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

                return Ok(new {
                    AccessToken = stringToken,
                    Role = user.Role,
                    RefreshToken = refreshToken,
                    CompanyId = user.CompanyId,
            });
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("relogin")]
        public IActionResult ReLogin()
        {
            var roles = User.FindAll(ClaimTypes.Role)?.Select(c => c.Value);

            return Ok(roles);
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpPost]
        [Route("refresh-token/{companyId}")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel, Guid companyId )
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            var loginDTO = new AccountDTO()
            {
                UserName = username
            };
            var user = await _userService.FindAccountAsync(loginDTO);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            //await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [HttpGet]
        [Route("new-access-token")]
        public async Task<IActionResult> GetNewAccessTokenAsync()
        {
            string? cookie = HttpContext.Request.Cookies["refreshToken"];
            return Ok(new {
                cookie
            });
        }

        [HttpPost]
        [Route("confirm-email")]
        public IActionResult SendVerificationEmail(string email)
        {
            

            try
            {
                var emailMimeMessage = new MimeMessage();
                emailMimeMessage.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSettings:EmailUsername").Value));
                emailMimeMessage.To.Add(MailboxAddress.Parse(email));
                emailMimeMessage.Subject = "Xin chào";
                emailMimeMessage.Body = new TextPart(TextFormat.Html) { Text = "Xin chào" };

                var smtp = new SmtpClient();
                smtp.Connect(_configuration.GetSection("EmailSettings:EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailSettings:EmailUsername").Value, _configuration.GetSection("EmailSettings:EmailPassword").Value);
                smtp.Send(emailMimeMessage);
                smtp.Disconnect(true);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }
        public static string GenerateVerificationCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        }


        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

    }


    public class TokenModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
