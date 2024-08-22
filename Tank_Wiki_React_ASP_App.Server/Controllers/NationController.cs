using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.DTO;
using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NationController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;
        public NationController(db_TankWikiContext dBContext) => _dbContext = dBContext;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Nations
                            .Include(n => n.Tanks)//отримання всіх танків
                            .Select(n => new NationDTO(n)//створення відображення
                            {
                                Tanks = n.Tanks.Select(t => new TankDTOTruncated(t)).ToList()//додавання відображення танків
                            })
                            .ToListAsync());
        }

        //пошук нації по id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Nation? nation = await _dbContext.Nations.Include(n => n.Tanks).FirstOrDefaultAsync(n => n.NationId == id);

            if (nation == null) return NotFound("Nation not found");

            NationDTO nationDTO = new NationDTO(nation)
            {
                Tanks = nation.Tanks.Select(t => new TankDTOTruncated(t)).ToList()
            };

            return Ok(nationDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string nation)
        {
            if (!string.IsNullOrEmpty(nation))
            {
                Nation newNation = new Nation();
                newNation.NationName = nation;

                await _dbContext.Nations.AddAsync(newNation);
                await _dbContext.SaveChangesAsync();

                return Ok(_dbContext.Nations);
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string nationName)
        {
            Nation? nation = await _dbContext.Nations.FindAsync(id);
            if (nation == null) return NotFound("Nation not found.");

            nation.NationName = string.IsNullOrEmpty(nationName) ? nation.NationName : nationName;

            _dbContext.Nations.Update(nation);
            await _dbContext.SaveChangesAsync();

            return Ok("Update nation.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.Nations.Where(n => n.NationId == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Delete nation.");
        }
    }
}
