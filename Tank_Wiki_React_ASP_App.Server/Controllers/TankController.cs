using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TankWiki.DTO;
using TankWiki.Models;
using TankWiki.Models.ModelTank;

namespace TankWiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TankController : ControllerBase
    {

        private readonly MySqlDBContext _dbContext;
        private readonly ILogger<TankController> _logger;

        public TankController(ILogger<TankController> logger, MySqlDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tanks = await _dbContext.Tanks
                                        .Include(tt => tt.TankType)
                                        .Include(te => te.TankEngines)
                                        .ThenInclude(e => e.Engine)
                                        .Include(tr => tr.TankRadios)
                                        .ThenInclude(r => r.Radio)
                                        .Include(ts => ts.TankSuspensions)
                                        .ThenInclude(s => s.Suspension)
                                        .Include(tt => tt.TankTurrets)
                                        .ThenInclude(t => t.Turret)
                                        .ThenInclude(tg => tg.TurretGuns)
                                        .ThenInclude(g => g.Gun)
                                        .Include(n => n.Nation)
                                        .Include(a => a.Armor)
                                        .ToListAsync();

            var result =tanks.Select(t => new TankDTO(t)
            {
                Engines = t.TankEngines.Select(e => new EngineDTO(e.Engine)).ToList(),
                Radios = t.TankRadios.Select(r => new RadioDTO(r.Radio)).ToList(),
                Suspensions = t.TankSuspensions.Select(s => new SuspensionDTO(s.Suspension)).ToList(),
                Turrets = t.TankTurrets.Select(t => new TurretDTO(t.Turret)
                {
                    Guns = t.Turret.TurretGuns.Select(g => new GunDTO(g.Gun)).ToList(),
                }).ToList(),
            }).ToList();

            //return Ok(result);
            return this.StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet("{tankId}")]
        public async Task<IActionResult> GetById(int tankId)
        {
            Tank? tank = await _dbContext.Tanks
                                        .Include(tt => tt.TankType)
                                        .Include(te => te.TankEngines)
                                        .ThenInclude(e => e.Engine)
                                        .Include(tr => tr.TankRadios)
                                        .ThenInclude(r => r.Radio)
                                        .Include(ts => ts.TankSuspensions)
                                        .ThenInclude(s => s.Suspension)
                                        .Include(tt => tt.TankTurrets)
                                        .ThenInclude(t => t.Turret)
                                        .ThenInclude(tg => tg.TurretGuns)
                                        .ThenInclude(g => g.Gun)
                                        .Include(n => n.Nation)
                                        .Include(a => a.Armor)
                                        .FirstOrDefaultAsync(t => t.TankId == tankId);

            if (tank == null) return NotFound("Tank not found.");

            TankDTO tankDTO = new TankDTO(tank)
            {
                Engines = tank.TankEngines.Select(e => new EngineDTO(e.Engine)).ToList(),
                Radios = tank.TankRadios.Select(r => new RadioDTO(r.Radio)).ToList(),
                Suspensions = tank.TankSuspensions.Select(s => new SuspensionDTO(s.Suspension)).ToList(),
                Turrets = tank.TankTurrets.Select(t => new TurretDTO(t.Turret)
                {
                    Guns = t.Turret.TurretGuns.Select(g => new GunDTO(g.Gun)).ToList(),
                }).ToList(),
            };

            return Ok(tankDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name, int nationId, int tier, int hitPoints, bool status,
                                              long price, int typeId, int armorId, [FromForm] List<string> crew)
        {
            Tank tank = new Tank()
            {
                Name = name,
                NationId = nationId,
                Tier = tier,
                HitPoints = hitPoints,
                Status = status,
                Price = price,
                TypeId = typeId,
                ArmorId = armorId,
                Crew = crew,
            };

            await _dbContext.Tanks.AddAsync(tank);
            await _dbContext.SaveChangesAsync();

            return Ok("Танк додано.");
        }

        [HttpPut("{tankId}")]
        public async Task<IActionResult> Put(int id, string? name, int? nationId, int? tier, int? hitPoints, bool? status,
                                              long? price, int? typeId, int? armorId, [FromForm] List<string> crew)
        {
            Tank? tank = await _dbContext.Tanks.FindAsync(id);

            tank.Name = string.IsNullOrEmpty(name) ? tank.Name : name;
            tank.NationId = nationId != null && _dbContext.Nations.Any(n => n.NationId == nationId) ?
                                                                    (int)nationId : tank.NationId;
            tank.Tier = tier != null && tier > 0 && tier <= 10 ? (int)tier : tank.Tier;
            tank.HitPoints = (int)(hitPoints ?? tank.HitPoints);
            tank.Status = (bool)(status ?? tank.Status);
            tank.Price = (long)(price ?? tank.Price);
            tank.TypeId = typeId != null && _dbContext.TankTypes
                                            .Any(t => t.TankTypeId == typeId) ? (int)typeId : tank.TypeId;
            tank.ArmorId = armorId != null && _dbContext.Armors
                                                .Any(a => a.ArmorId == armorId) ? (int)armorId : tank.ArmorId;

            _dbContext.Tanks.Update(tank);
            await _dbContext.SaveChangesAsync();

            return Ok("Update tank.");
        }

        [HttpPatch("updateDescription/{tankId}")]
        public async Task<IActionResult> PatchDescription(int tankId, string description)
        {
            Tank? tank = await _dbContext.Tanks.FindAsync(tankId);
            if (tank == null) return NotFound("Tank not found");

            tank.Description = description;
            _dbContext.Tanks.Update(tank);
            await _dbContext.SaveChangesAsync();

            return Ok("Added description.");
        }

        [HttpDelete("{tankId}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.Tanks.Where(t=>t.TankId==id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Tank delete.");
        }
    }
}
