using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models;
using Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany;

namespace TankWiki.Controllers.ControllersManyToMany
{
    [Route("[controller]")]
    [ApiController]
    public class TankTurretsController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;

        public TankTurretsController(db_TankWikiContext dBContext) => _dbContext = dBContext;


        [HttpPost]
        public async Task<IActionResult> Post(int tankId, int turretId)
        {
            //перевіряе чи існуе танк
            var tank = await _dbContext.Tanks.FindAsync(tankId);
            if (tank == null) NotFound("Танк не знайдено.");
            //перевіряе чи існуе радіо
            var turret = await _dbContext.Turrets.FindAsync(turretId);
            if (turret == null) NotFound("Башня не знайдено.");

            string resultOperation;

            try
            {
                await _dbContext.TankTurrets.AddAsync(new TankTurret { Turret = turret, Tank = tank });
                await _dbContext.SaveChangesAsync();
                resultOperation = "Башня додано до танка.";
            }
            catch (Exception ex)
            {
                resultOperation = ex.Message;
                return BadRequest(resultOperation);
            }

            return Ok(resultOperation);
        }

        // Замінює oldTurretId на newTurretId в таблиці TankTurrets
        [HttpPut("update_turrets_id/")]
        public async Task<IActionResult> UpdateTurretsId(int oldTurretId, int newTurretId)
        {
            if (oldTurretId < 0 || !await _dbContext.TankTurrets.AnyAsync(tt => tt.TurretId == oldTurretId))
                return BadRequest("Wrong turret id.");

            await _dbContext.TankTurrets
                            .Where(el => el.TurretId == oldTurretId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.TurretId, newTurretId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update turret id.");
        }

        // Замінює oldTankId на newTankId в таблиці TankTurrets
        [HttpPut("update_tank_id")]
        public async Task<IActionResult> UpdateTankId(int oldTankId, int newTankId)
        {
            if (oldTankId < 0 || !await _dbContext.TankTurrets.AnyAsync(tt => tt.TankId == oldTankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankTurrets
                            .Where(el => el.TankId == oldTankId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.TankId, newTankId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update tank id.");
        }

        //видаляе всі рядки в яких зустрічается значення tankId в таблиці TankTurrets
        [HttpDelete("delete_tank/{tankId}")]
        public async Task<IActionResult> DeleteTanksById(int tankId)
        {
            if (tankId < 0 || !await _dbContext.TankTurrets.AnyAsync(tt => tt.TankId == tankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankTurrets
                            .Where(tt => tt.TankId == tankId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted Tank.");
        }

        //видаляе всі рядки в яких зустрічается значення turretId в таблиці TankTurrets
        [HttpDelete("delete_turret/{turretId}")]
        public async Task<IActionResult> DeleteTurretById(int turretId)
        {
            if (turretId < 0 || !await _dbContext.TankTurrets.AnyAsync(tt => tt.TurretId == turretId))
                return BadRequest("Wrong turret id.");

            await _dbContext.TankTurrets
                            .Where(tt => tt.TurretId == turretId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted turret.");
        }


    }
}
