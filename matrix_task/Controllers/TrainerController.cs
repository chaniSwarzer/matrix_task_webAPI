using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using matrix_task.Entities;
using matrix_task.Filters;
using matrix_task.Helpers;
using matrix_task.Model.Trainer;
using matrix_task.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace matrix_task.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {

        private ITrainerService _trainerService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TrainerController(ITrainerService trainerService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _trainerService = trainerService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult test()
        {

            return Ok(new
            {
                mes = "it is work!! :)"
            });
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [TypeFilter(typeof(ExceptionFilter))]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var trainer = _trainerService.Authenticate(model.Username, model.Password);

            if (trainer == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, trainer.TrainerId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic trainer info and authentication token
            return Ok(new
            {
                Id = trainer.TrainerId,
                Username = trainer.UserName,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var trainer = _mapper.Map<Trainer>(model);

            try
            {
                // create trainer
                _trainerService.Create(trainer, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
