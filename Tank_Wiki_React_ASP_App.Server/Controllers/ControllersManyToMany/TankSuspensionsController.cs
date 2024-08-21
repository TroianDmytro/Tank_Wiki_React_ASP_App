using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models;
using Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany;

namespace TankWiki.Controllers.ControllersManyToMany
{
    [Route("[controller]")]
    [ApiController]
    public class TankSuspensionsController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;

        public TankSuspensionsController(db_TankWikiContext dBContext) => _dbContext = dBContext;


        [HttpPost]
        public async Task<IActionResult> Post(int tankId, int suspensionId)
        {
            //перевіряе чи існуе танк
            var tank = await _dbContext.Tanks.FindAsync(tankId);
            if (tank == null) NotFound("Танк не знайдено.");
            //перевіряе чи існуе радіо
            var suspension = await _dbContext.Suspensions.FindAsync(suspensionId);
            if (suspension == null) NotFound("Трансмісія не знайдено.");

            string resultOperation;

            try
            {
                await _dbContext.TankSuspensions.AddAsync(new TankSuspension { Suspension = suspension, Tank = tank });
                await _dbContext.SaveChangesAsync();
                resultOperation = "Трансмісія додано до танка.";
            }
            catch (Exception ex)
            {
                resultOperation = ex.Message;
                return BadRequest(resultOperation);
            }

            return Ok(resultOperation);
        }

        // Замінює oldSuspensionId на newSuspensionId в таблиці TankSuspensions
        [HttpPut("update_suspensions_id")]
        public async Task<IActionResult> UpdateSuspensionsId(int oldSuspensionId, int newSuspensionId)
        {
            if (oldSuspensionId < 0 || !await _dbContext.TankSuspensions.AnyAsync(ts => ts.SuspensionId == oldSuspensionId))
                return BadRequest("Wrong suspension id.");

            await _dbContext.TankSuspensions
                            .Where(el => el.SuspensionId == oldSuspensionId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.SuspensionId, newSuspensionId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update suspension id.");
        }

        // Замінює oldTankId на newTankId в таблиці TankSuspensions
        [HttpPut("update_tank_id")]
        public async Task<IActionResult> UpdateTankId(int oldTankId, int newTankId)
        {
            if (oldTankId < 0 || !await _dbContext.TankSuspensions.AnyAsync(ts => ts.TankId == oldTankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankSuspensions
                            .Where(el => el.TankId == oldTankId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.TankId, newTankId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update tank id.");
        }

        //видаляе всі рядки в яких зустрічается значення tankId в таблиці TankSuspensions
        [HttpDelete("delete_tank/{tankId}")]
        public async Task<IActionResult> DeleteTanksById(int tankId)
        {
            if (tankId < 0 || !await _dbContext.TankSuspensions.AnyAsync(ts => ts.TankId == tankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankSuspensions
                            .Where(ts => ts.TankId == tankId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted Tank.");
        }

        //видаляе всі рядки в яких зустрічается значення suspensionId в таблиці TankSuspensions
        [HttpDelete("delete_suspension/{suspensionId}")]
        public async Task<IActionResult> DeleteSuspensionById(int suspensionId)
        {
            if (suspensionId < 0 || !await _dbContext.TankSuspensions.AnyAsync(ts => ts.SuspensionId == suspensionId))
                return BadRequest("Wrong suspension id.");

            await _dbContext.TankSuspensions
                            .Where(ts => ts.SuspensionId == suspensionId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted suspension.");
        }

    }
}
