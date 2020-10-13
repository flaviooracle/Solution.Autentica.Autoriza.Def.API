using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Solution.Autentica.Autoriza.Def.API.Contract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Solution.Autentica.Autoriza.Def.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
  // 
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{nome}/{senha}")]
        [Authorize()]
        [AllowAnonymous] // referencia de qualquer usuario pode acessar

        public IActionResult RequestToken(string nome, string senha)
        {
            Request request = new Request() { Nome = nome, Senha = senha };
            if (request.Nome == "faom" && request.Senha == "faom")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Nome)
                };

                // recebe uma instancia da classe SymmetricSecurityKey
                // armazenando a chave de criptografia usada na criação do token
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                //recebe um objeto do tipo SigninCredentials contendo a chave de
                //criptografia e o algoritmo de segurança empregados na geração
                //de assinaturas digitais para tokens
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //gerar o token
                var token = new JwtSecurityToken(
                    issuer: "flavio",
                    audience:"miranda",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Ok("Sem Token");
        }

        [HttpGet]
        [Authorize()]
        public IActionResult Sinal()
        {
            return Ok("Cheguei no Get");
        }
    }
}
