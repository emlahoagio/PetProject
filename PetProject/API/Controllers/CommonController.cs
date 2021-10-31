using API.RequestModels.Commons;
using API.ResponseModels.Accounts;
using AutoMapper;
using Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Define;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public CommonController(IAccountService accountService, IConfiguration configuration, IMapper mapper)
        {
            _accountService = accountService;
            _config = configuration;
            _mapper = mapper;
        }
        private string GenerateJSONWebToken(AccountResponse userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("email", userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userinfo.Role)
            };

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(15),
                    signingCredentials: credentials
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel login)
        {
            try
            {

                var account = _accountService.Get(x => x.Username.Equals(login.Username) && x.Password.Equals(login.Password), null);
                if (account == null)
                {
                    return StatusCode(405, "Login Failed!");
                }
                else
                {
                    var accountResponse = _mapper.Map<AccountResponse>(account);
                    accountResponse.Role = ((CommonEnum.Role)account.Role).ToString();
                    var token = GenerateJSONWebToken(accountResponse);
                    return StatusCode(200, new
                    {
                        mess = "Login Success",
                        token = token,
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
