﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models;


namespace Tank_Wiki_React_ASP_App.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TankTypeController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;
        public TankTypeController(db_TankWikiContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.TankTypes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(string tankType)
        {
            if (!string.IsNullOrEmpty(tankType))
            {
                TankType newTankType = new TankType();
                newTankType.TypeMachine = tankType;
                await _dbContext.TankTypes.AddAsync(newTankType);
                await _dbContext.SaveChangesAsync();

                return Ok("Added tank type.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.TankTypes.Where(tt => tt.TankTypeId == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Delete tank type.");
        }

    }
}
