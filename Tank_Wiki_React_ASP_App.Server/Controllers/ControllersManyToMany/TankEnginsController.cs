using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Tank_Wiki_React_ASP_App.Server.Models;
using Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany;

namespace Tank_Wiki_React_ASP_App.Server.Controllers.ControllersManyToMany
{
    [Route("[controller]")]
    [ApiController]
    public class TankEnginsController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;

        public TankEnginsController(db_TankWikiContext dBContext) => _dbContext = dBContext;

        [HttpPost]
        public async Task<IActionResult> Post(int tankId, int engineId)
        {
            //перевіряе чи існуе танк
            var tank = await _dbContext.Tanks.FindAsync(tankId);
            if (tank == null) NotFound("Танк не знайдено.");
            //перевіряе чи існуе двигун
            var engine = await _dbContext.Engines.FindAsync(engineId);
            if (engine == null) NotFound("Двигун не знайдено.");

            string resultOperation;

            try
            {
                await _dbContext.TankEngines.AddAsync(new TankEngine { Engine = engine, Tank = tank });
                await _dbContext.SaveChangesAsync();
                resultOperation = "Двигун додано до танка.";
            }
            catch (Exception ex)
            {
                resultOperation = ex.Message;
                return BadRequest(resultOperation);
            }

            return Ok(resultOperation);
        }

        // Замінює oldEngineId на newEngineId в таблиці TankEngines
        [HttpPut("update_engine_id")]
        public async Task<IActionResult> UpdateEngineId(int oldEngineId, int newEngineId)
        {
            if (newEngineId < 0 || !await _dbContext.TankEngines.AnyAsync(t => t.EngineId == newEngineId))
                return BadRequest("Wrong engine id.");

            await _dbContext.TankEngines
                            .Where(el => el.EngineId == oldEngineId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.EngineId, newEngineId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update engine id.");
        }

        // Замінює oldTankId на newTankId в таблиці TankEngines
        [HttpPut("update_tank_id")]
        public async Task<IActionResult> UpdateTankId(int oldTankId, int newTankId)
        {
            if (oldTankId < 0 || !await _dbContext.TankEngines.AnyAsync(t => t.TankId == oldTankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankEngines
                            .Where(el => el.TankId == oldTankId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.TankId, newTankId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update tank id.");
        }

        //видаляе всі рядки в яких зустрічается значення tankId
        [HttpDelete("DeleteTank/{tankId}")]
        public async Task<IActionResult> DeleteTanksById(int tankId)
        {
            if (tankId < 0 || !await _dbContext.TankEngines.AnyAsync(t => t.TankId == tankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankEngines
                            .Where(te => te.TankId == tankId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted Tank.");
        }

        //видаляе всі рядки в яких зустрічается значення engineId
        [HttpDelete("DeleteEngine/{engineId}")]
        public async Task<IActionResult> DeleteEngineById(int engineId)
        {
            if (engineId < 0 || !await _dbContext.TankEngines.AnyAsync(t => t.EngineId == engineId))
                return BadRequest("Wrong engine id.");

            await _dbContext.TankEngines
                            .Where(te => te.EngineId == engineId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted engine.");
        }
    }
}
