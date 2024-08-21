using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TankWiki.DTO;
using TankWiki.Models;
using TankWiki.Models.ModelOneToMany;
using TankWiki.Models.ModelTank;

namespace TankWiki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TurretController : ControllerBase
    {
        private readonly MySqlDBContext _dbContext;

        public TurretController(MySqlDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTurrets()
        {
            var turrets = await _dbContext.Turrets
                .AsNoTracking()
                .Include(t => t.TurretGuns)
                .ThenInclude(g => g.Gun)
                .Include(tg => tg.TankTurrets)
                .ThenInclude(tn => tn.Tank)
                .Select(t => new TurretDTO(t)
                {
                    Guns = t.TurretGuns
                            .Select(tg => new GunDTO(tg.Gun))
                            .ToList(),
                    Tanks = t.TankTurrets
                             .Select(t => new TankDTOTruncated(t.Tank))
                             .ToList()

                }).ToListAsync();

            //return this.StatusCode((int)HttpStatusCode.OK, turrets);
            return Ok(turrets);
        }

        [HttpGet("{turretId}")]
        public async Task<IActionResult> GetById(int turretId)
        {
            var turret = await _dbContext.Turrets
                                        .Include(tg => tg.TurretGuns)
                                        .ThenInclude(g => g.Gun)
                                        .Include(tt => tt.TankTurrets)
                                        .ThenInclude(t => t.Tank)
                                        .FirstOrDefaultAsync(t => t.TurretId == turretId);

            if (turret == null) return BadRequest("Turret not found.");
          
            var newTurret = new TurretDTO(turret)
            {
                Guns = turret.TurretGuns
                             .Select(gun => new GunDTO(gun.Gun))
                             .ToList(),
                
                Tanks = turret.TankTurrets
                              .Select(t => new TankDTOTruncated(t.Tank))
                              .ToList()
            };

            return Ok(turret);
        }

        [HttpPost]
        public async Task<IActionResult> PostTurret(
                                string turretName,int tier, int turretFront, int turretSide, 
                                int turretRear, double turn, int overview, int weight, long price)
        {
            var turret = new Turret
            {
                TurretName = turretName,
                Tier = tier,
                TurretFront = turretFront,
                TurretSide = turretSide,
                TurretRear = turretRear,
                Turn = turn,
                Overview = overview,
                Weight = weight,
                Price = price
            };

            await _dbContext.Turrets.AddAsync(turret);
            await _dbContext.SaveChangesAsync();

            return Ok("Added turret.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string? turretName, int? tier, int? turretFront,
                                             int? turretSide, int? turretRear, double? turn, int? overview,
                                             int? weight, long? price)
        {
            Turret? turret = await _dbContext.Turrets.FindAsync(id);

            if (turret == null) return BadRequest("Turret not found.");

            turret.TurretName = string.IsNullOrEmpty(turretName) || turretName.Equals(turret.TurretName) ?
                                                                            turret.TurretName : turretName;
            turret.Tier =(int)( tier??turret.Tier);
            turret.TurretFront = (int)(turretFront ?? turret.TurretFront);
            turret.TurretSide = (int)(turretSide ?? turret.TurretSide);
            turret.TurretRear = (int)(turretRear ?? turret.TurretRear);
            turret.Turn = (double)(turn ?? turret.Turn);
            turret.Overview = (int)(overview ?? turret.Overview);
            turret.Weight = (int)(weight ?? turret.Weight);
            turret.Price = (long)(price ?? turret.Price);

            _dbContext.Turrets.Update(turret);
            await _dbContext.SaveChangesAsync();

            return Ok("Updated turret");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.Turrets.Where(t=>t.TurretId == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted turret.");
        }

    }
}
