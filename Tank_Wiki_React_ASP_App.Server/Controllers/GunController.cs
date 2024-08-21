using Microsoft.AspNetCore.Mvc;
using TankWiki.Models;
using TankWiki.Models.ModelTank;
using Microsoft.EntityFrameworkCore;
using TankWiki.DTO;


namespace TankWiki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GunController : ControllerBase
    {
        private readonly MySqlDBContext _dbContext;

        public GunController(MySqlDBContext dBContext) => _dbContext = dBContext;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Guns
                .Include(t => t.TurretGuns)
                .ThenInclude(t => t.Turret)
                .Select(gun => new GunDTO(gun)
                {
                    Turret = gun.TurretGuns.Select(tg => new TurretDTO(tg.Turret))
                                           .ToList()
                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Gun? gun = await _dbContext.Guns
                                        .Include(t => t.TurretGuns)
                                        .ThenInclude(t => t.Turret)
                                        .FirstOrDefaultAsync(g => g.GunId == id);

            if (gun == null) return NotFound("Gun not found.");

            GunDTO gunDTO = new GunDTO(gun)
            {
                Turret = gun.TurretGuns
                            .Select(tg => new TurretDTO(tg.Turret))
                            .ToList()
            };

            return Ok(gunDTO);

        }

        [HttpPost]
        public async Task<IActionResult> Post(int tier, string name, int penetration,
                                              int damage, double rateOfFire, double accuracy,
                                              double aimTime, int ammunition, double weight,
                                              long price)
        {
            Gun gun = new Gun()
            {
                Tier = tier,
                Name = name,
                Penetration = penetration,
                Damage = damage,
                RateOfFire = rateOfFire,
                Accuracy = accuracy,
                AimTime = aimTime,
                Ammunition = ammunition,
                Weight = weight,
                Price = price
            };

            await _dbContext.Guns.AddAsync(gun);
            await _dbContext.SaveChangesAsync();

            return Ok(gun);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, int? tier, string? name, int? penetration,
                                              int? damage, double? rateOfFire, double? accuracy,
                                              double? aimTime, int? ammunition, double? weight,
                                              long? price)
        {
            Gun? gun = await _dbContext.Guns.FindAsync(id);
            if (gun == null) return NotFound("Гармата не знайдена.");

            gun.Tier = tier == null ? gun.Tier : (int)tier;
            gun.Name = string.IsNullOrEmpty(name) ? gun.Name : name;
            gun.Penetration = penetration == null ? gun.Penetration : (int)penetration;
            gun.Damage = damage == null ? gun.Damage : (int)damage;
            gun.RateOfFire = rateOfFire == null ? gun.RateOfFire : (double)rateOfFire;
            gun.Accuracy = accuracy == null ? gun.Accuracy : (double)accuracy;
            gun.AimTime = aimTime == null ? gun.AimTime : (double)aimTime;
            gun.Ammunition = ammunition == null ? gun.Ammunition : (int)ammunition;
            gun.Weight = weight == null ? gun.Weight : (double)weight;
            gun.Price = price == null ? gun.Price : (long)price;

            _dbContext.Guns.Update(gun);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.Guns.Where(g=>g.GunId==id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

