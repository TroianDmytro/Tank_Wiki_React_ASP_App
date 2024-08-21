using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models;
using Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany;

namespace TankWiki.Controllers.ControllersManyToMany
{
    [Route("[controller]")]
    [ApiController]
    public class TankRadiosController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;

        public TankRadiosController(db_TankWikiContext dBContext) => _dbContext = dBContext;


        [HttpPost]
        public async Task<IActionResult> Post(int tankId, int radioId)
        {
            //перевіряе чи існуе танк
            var tank = await _dbContext.Tanks.FindAsync(tankId);
            if (tank == null) NotFound("Танк не знайдено.");
            //перевіряе чи існуе радіо
            var radio = await _dbContext.Radios.FindAsync(radioId);
            if (radio == null) NotFound("Радіо не знайдено.");

            string resultOperation;

            try
            {
                await _dbContext.TankRadios.AddAsync(new TankRadio { Radio = radio, Tank = tank });
                await _dbContext.SaveChangesAsync();
                resultOperation = "Радіо додано до танка.";
            }
            catch (Exception ex)
            {
                resultOperation = ex.Message;
                return BadRequest(resultOperation);
            }

            return Ok(resultOperation);
        }

        // Замінює oldRadioId на newRadioId в таблиці TankRadios
        [HttpPut("update_radio_id")]
        public async Task<IActionResult> UpdateRadioId(int oldRadioId, int newRadioId)
        {
            if (oldRadioId < 0 || !await _dbContext.TankRadios.AnyAsync(tr => tr.RadioId == oldRadioId))
                return BadRequest("Wrong radio id.");

            await _dbContext.TankRadios
                            .Where(el => el.RadioId == oldRadioId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.RadioId, newRadioId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update radio id.");
        }

        // Замінює oldTankId на newTankId в таблиці TankRadios
        [HttpPut("update_tank_id")]
        public async Task<IActionResult> UpdateTankId(int oldTankId, int newTankId)
        {
            if (oldTankId < 0 || !await _dbContext.TankRadios.AnyAsync(tr => tr.TankId == oldTankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankRadios
                            .Where(el => el.TankId == oldTankId)
                            .ExecuteUpdateAsync(g => g.SetProperty(p => p.TankId, newTankId));
            await _dbContext.SaveChangesAsync();

            return Ok("Update tank id.");
        }

        //видаляе всі рядки в яких зустрічается значення tankId в таблиці TankRadios
        [HttpDelete("delete_tank/{tankId}")]
        public async Task<IActionResult> DeleteTanksById(int tankId)
        {
            if (tankId < 0 || !await _dbContext.TankRadios.AnyAsync(tr => tr.TankId == tankId))
                return BadRequest("Wrong tank id.");

            await _dbContext.TankRadios
                            .Where(tr => tr.TankId == tankId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted Tank.");
        }

        //видаляе всі рядки в яких зустрічается значення radioId в таблиці TankRadios
        [HttpDelete("delete_radio/{radioId}")]
        public async Task<IActionResult> DeleteRadioById(int radioId)
        {
            if (radioId < 0 || !await _dbContext.TankRadios.AnyAsync(tr => tr.RadioId == radioId))
                return BadRequest("Wrong radio id.");

            await _dbContext.TankRadios
                            .Where(tr => tr.RadioId == radioId)
                            .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Deleted radio.");
        }
    }
}
