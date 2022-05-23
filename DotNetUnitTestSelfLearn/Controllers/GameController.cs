using AutoMapper;
using DotNetUnitTestSelfLearn.Data;
using DotNetUnitTestSelfLearn.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetUnitTestSelfLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;



        public GameController(IGeneralRepository generalRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _generalRepository = generalRepository;
            _mapper = mapper;
            _env = env;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<List<GameModel>>> GetAllExistingGames()
        {
            var games = await _generalRepository.GetAllGames();
            return Ok(new
            {
                Status = 200,
                Message = "Success",
                Result = games
            });
        }

        // GET: api/Project
        [HttpGet("{id}")]
        public async Task<ActionResult<GameModel>> GetExistingGameByID(int id)
        {
            var game = await _generalRepository.GetGameByID(id);
            if (game == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Game not found!"
                });
            }

            return Ok(new
            {
                Status = 200,
                Message = "Success",
                Result = game
            });

        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<GameModel>> AddGames([FromBody] GameModel gameObj)
        {
            if (gameObj == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Please input data",

                });
            }

            var doesGameExist = await _generalRepository.GetGameByGameName(gameObj.GameName);

            if (doesGameExist != null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Game Already exists",

                });
            }

            await _generalRepository.AddGameAsync(gameObj);
            await _generalRepository.SaveChangesAsync();

            return Ok(new
            {
                Status = 200,
                Message = "Success - Game Added!",
                Result = gameObj
            });
        }
    }
}
