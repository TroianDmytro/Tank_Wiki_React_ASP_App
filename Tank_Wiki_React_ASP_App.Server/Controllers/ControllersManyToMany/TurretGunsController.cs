using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TankWiki.Models;
using TankWiki.Models.ModelOneToMany;

namespace TankWiki.Controllers.ControllersManyToMany
{
    [Route("[controller]")]
    [ApiController]
    public class TurretGunsController : ControllerBase
    {
        private readonly MySqlDBContext _dbContext;

        public TurretGunsController(MySqlDBContext dBContext) => _dbContext = dBContext;

        // Post: TurretGuns/Add
        [HttpPost]
        public async Task<IActionResult> AddTurretToGun(int gunId, int turretId)
        {
            // Перевірка, чи існує гармата
            var gun = await _dbContext.Guns.FindAsync(gunId);

            if (gun == null) return NotFound("Гармата не знайдена.");

            // Перевірка, чи існує башта
            var turret = await _dbContext.Turrets.FindAsync(turretId);
            if (turret == null) return NotFound("Башта не знайдена.");

            string resultOperation;
            try
            {
                // Додавання нового зв'язку
                await _dbContext.TurretGuns.AddAsync(new TurretGun() { Gun = gun, Turret = turret });
                // Збереження змін у базі даних
                await _dbContext.SaveChangesAsync();
                resultOperation = "Башта успішно додана до гармати.";
            }
            catch (Exception ex)
            {
                resultOperation = ex.Message;
                return BadRequest(resultOperation);
            }

            return Ok(resultOperation);
        }

        // Замінює oldTurretId на newTurretId в таблиці TurretGuns
        [HttpPut("update_gun_id")]
        public async Task<IActionResult> UpdateGunId(int oldGunId, int newGunId)
        {
            await _dbContext.TurretGuns
                            .Where(el=>el.GunId==oldGunId)
                            .ExecuteUpdateAsync(g=>g.SetProperty(p=>p.GunId,newGunId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update gun id.");
        }

        // Замінює oldTurretId на newTurretId в таблиці TurretGuns
        [HttpPut("update_turret_id")]
        public async Task<IActionResult> UpdateTurretId(int oldTurretId, int newTurretId)
        {
            await _dbContext.TurretGuns
                            .Where(el => el.TurretId == oldTurretId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.TurretId, newTurretId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update turret id.");
        }

        //видаляе всі колонки в яких зустрічается gunId
        [HttpDelete("DeleteGan/{gunId}")]
        public async Task<IActionResult> DeleteGunsById(int gunId)
        {
            await _dbContext.TurretGuns
                            .Where(tg => tg.GunId == gunId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted Gan.");
        }

        //видаляе всі колонки в яких зустрічается turretId
        [HttpDelete("DeleteTurret/{turretId}")]
        public async Task<IActionResult> DeleteTurretsById(int turretId)
        {
            await _dbContext.TurretGuns
                            .Where(tg => tg.TurretId == turretId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted turret.");
        }
    }
}
