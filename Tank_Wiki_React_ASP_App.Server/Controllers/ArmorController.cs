using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TankWiki.DTO;
using TankWiki.Models;
using TankWiki.Models.ModelTank;

namespace TankWiki.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class ArmorController : ControllerBase
    {
        private readonly MySqlDBContext _dbContext;
        public ArmorController(MySqlDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list = await _dbContext.Armors
                .Include(a => a.Tank)
                .ThenInclude(t => t.Nation)
                .Include(a => a.Tank)
                .ThenInclude(t => t.TankType)
                .Select(armor => new ArmorDTO(armor)
                {
                    Tanks = armor.Tank == null ? null : new TankDTOTruncated(armor.Tank)
                }).ToListAsync();

            return Ok(list);

        }

        [HttpGet("{armorid}")]
        public async Task<IActionResult> GetById(int armorid)
        {
            Armor? armor = await _dbContext.Armors
                                           .AsNoTracking()
                                           .Include(a => a.Tank)
                                           .ThenInclude(t => t.Nation)
                                           .Include(a => a.Tank)
                                           .ThenInclude(t => t.TankType)
                                           .FirstOrDefaultAsync(a => a.ArmorId == armorid);

            if (armor == null) return NotFound("Armor not found");

            ArmorDTO armorDTO = new ArmorDTO(armor)
            {
                Tanks = armor.Tank == null ? null : new TankDTOTruncated(armor.Tank)
            };

            return Ok(armorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name, int front, int side, int rear, int? tankId)
        {
            Armor armor = new Armor()
            {
                Name = name,
                HullFront = front,
                HullSide = side,
                HullRear = rear,
            };

            if (tankId!=null && _dbContext.Tanks.Any(t=>t.TankId==tankId)) armor.TankId = tankId;

            await _dbContext.Armors.AddAsync(armor);
            await _dbContext.SaveChangesAsync();

            return Ok("Add armor.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string? name, int? front, int? side, int? rear, int? tankId)
        {
            var armor = await _dbContext.Armors
                .FirstOrDefaultAsync(a => a.ArmorId == id);
            
            if (armor == null) return BadRequest();
            
            armor.Name = string.IsNullOrEmpty(name) ? armor.Name : name;
            armor.HullFront = front == null ? armor.HullFront : (int)front;
            armor.HullSide = side == null ? armor.HullSide : (int)side;
            armor.HullRear = rear == null ? armor.HullRear : (int)rear;
            armor.TankId = tankId == null ? armor.TankId : (int)tankId;

            _dbContext.Armors.Update(armor);
            await _dbContext.SaveChangesAsync();

            return Ok("Update armor.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Armor armor = await _dbContext.Armors.FirstOrDefaultAsync(a => a.ArmorId == id);

            if (armor == null) return BadRequest();

            _dbContext.Armors.Remove(armor);
            await _dbContext.SaveChangesAsync();
            return Ok("Delete armor.");
        }
    }

}
