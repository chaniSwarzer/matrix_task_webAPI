using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using matrix_task.Entities;
using matrix_task.Model.Hero;
using matrix_task.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace matrix_task.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private ITrainerService _trainerService;
        private IMapper _mapper;
        public HeroController(ITrainerService trainerService, IMapper mapper)
        {
            _trainerService = trainerService;
            _mapper = mapper;
        }

        [HttpGet("GetHerosByTrainer/{TrainerId}")]
        public IActionResult GetHerosByTrainer([FromRoute]int TrainerId)
        {
            var heros = _trainerService.GetHerosByTrainer(TrainerId);

            var res = _mapper.Map<IEnumerable<HeroModel>>(heros);
            return Ok(res);
        }
    }
}
