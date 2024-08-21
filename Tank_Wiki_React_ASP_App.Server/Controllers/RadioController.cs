using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TankWiki.DTO;
using TankWiki.Models;
using TankWiki.Models.ModelOneToMany;
using TankWiki.Models.ModelTank;

namespace TankWiki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RadioController : ControllerBase
    {
        private readonly MySqlDBContext _dbContext;
        public RadioController(MySqlDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var radio = await _dbContext.Radios
                .Include(tr => tr.TankRadios)
                .ThenInclude(t => t.Tank)
                .Select(r => new RadioDTO(r)
                {
                    Tanks = r.TankRadios.Select(t => new TankDTOTruncated(t.Tank)).ToList()
                }).ToListAsync();

            return Ok(radio);
        }

        [HttpGet("{radioId}")]
        public async Task<IActionResult> GetRadioById(int radioId)
        {
            Radio? radio = await _dbContext.Radios
                                .Include(tr => tr.TankRadios)
                                .ThenInclude(t => t.Tank)
                                .FirstOrDefaultAsync(r => r.RadioId == radioId);

            if (radio == null) return NotFound("Radio not found");

            RadioDTO? result = new RadioDTO(radio)
            {
                Tanks = radio.TankRadios.Select(t => new TankDTOTruncated(t.Tank)).ToList()
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name, int tier, int signalRange, int weiht, long price)
        {
            Radio radio = new Radio()
            {
                Name = name,
                Tier = tier,
                SignalRange = signalRange,
                Weight = weiht,
                Price = price
            };

            await _dbContext.Radios.AddAsync(radio);
            await _dbContext.SaveChangesAsync();

            return Ok("Радіо додано.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string? name, int? tier, int? signalRange, int? weiht, long? price)
        {
            Radio? radio = await _dbContext.Radios.FindAsync(id);

            if (radio == null) return NotFound("Radio not found.");

            radio.Name = string.IsNullOrEmpty(name) ? radio.Name : name;
            radio.Tier = tier == null ? radio.Tier : (int)tier;
            radio.SignalRange = signalRange == null ? radio.SignalRange : (int)signalRange;
            radio.Weight = weiht == null ? radio.Weight : (int)weiht;
            radio.Price = price == null ? radio.Price : (long)price;

            _dbContext.Radios.Update(radio);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.Radios.Where(r=>r.RadioId==id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
