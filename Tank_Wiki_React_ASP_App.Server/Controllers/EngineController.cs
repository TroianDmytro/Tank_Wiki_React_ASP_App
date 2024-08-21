using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.DTO;
using Tank_Wiki_React_ASP_App.Server.Models;


namespace TankWiki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;
        public EngineController(db_TankWikiContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Engines
                .Include(t => t.TankEngines )
                .ThenInclude(t => t.Tank)
                .Select(e => new EngineDTO(e)
                {
                    Tanks = e.TankEngines
                             .Select(te => new TankDTOTruncated(te.Tank))
                             .ToList()
                })
                .ToListAsync());
        }

        [HttpGet("{engineId}")]
        public async Task<IActionResult> GetEngineId(int engineId)
        {
            var engine = await _dbContext.Engines
                                        .AsNoTracking()
                                        .Include(e => e.TankEngines)
                                        .ThenInclude(te => te.Tank)
                                        .FirstOrDefaultAsync(e => e.EngineId == engineId);

            if (engine == null) return BadRequest();

            return Ok(new EngineDTO(engine)
            {
                Tanks = engine.TankEngines
                               .Select(te => new TankDTOTruncated(te.Tank) { })
                               .ToList()
            });
        }

        //Додавання двигуна в db
        [HttpPost]
        public async Task<IActionResult> Post(string name, int tier,
                                int power, double fireChance, double weight,
                                long price, [FromForm] List<int> tankIds)
        {
            Engine engine = new Engine()
            {
                Name = name,
                Tier = tier,
                Power = power,
                FireChance = fireChance,
                Weight = weight,
                Price = price
            };

            await _dbContext.Engines.AddAsync(engine);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{engineId}")]
        public async Task<IActionResult> Put(int engineId, string? name, int? tier,
                                            int? power, double? fireChance, double? weight,
                                            long? price)
        {
            var engine = await _dbContext.Engines.FindAsync(engineId);

            if (engine == null) return NotFound("Engine not found.");

            engine.Name = name == null ? engine.Name : name;
            engine.Tier = tier == null ? engine.Tier : (int)tier;
            engine.Power = power == null ? engine.Power : (int)power;
            engine.FireChance = fireChance == null ? engine.FireChance : (int)fireChance;
            engine.Weight = weight == null ? engine.Weight : (int)weight;
            engine.Price = price == null ? engine.Price : (int)price;

            _dbContext.Engines.Update(engine);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            await _dbContext.Engines.Where(e => e.EngineId == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

