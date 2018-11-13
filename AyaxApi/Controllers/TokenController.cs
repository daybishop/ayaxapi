using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AyaxApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AyaxApi.Controllers
{
    [Route("token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        [HttpPost]
        public IActionResult Create([FromBody] LoginInput inputModel)
        {
            if (inputModel.Username != "ayax" || inputModel.Password != "ayax")
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, inputModel.Username)
            };

            var token = new JwtSecurityToken(
                issuer: "ayax",
                audience: "ayax",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ayaxayaxayaxayaxayaxayaxayaxayaxayaxayax")),
                        SecurityAlgorithms.HmacSha256
                    )
            );

            return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                }
            );
        }
    }
}