using Microsoft.AspNetCore.Http;
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
    public class SuspensionController : ControllerBase
    {
        private readonly MySqlDBContext _dbContext;
        public SuspensionController(MySqlDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<SuspensionDTO> suspension = await _dbContext.Suspensions
                                             .Include(ts => ts.TankSuspensions)
                                             .ThenInclude(t => t.Tank)
                                             .Select(s => new SuspensionDTO(s)
                                             {
                                                 Tanks = s.TankSuspensions.Select(t => new TankDTOTruncated(t.Tank)).ToList()
                                             })
                                             .ToListAsync();
            return Ok(suspension);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int id)
        {
            Suspension? suspension = await _dbContext.Suspensions
                                             .Include(ts => ts.TankSuspensions)
                                             .ThenInclude(t => t.Tank)
                                             .FirstOrDefaultAsync(s => s.SuspensionId == id);

            if (suspension == null) return NotFound("Suspension not found.");

            SuspensionDTO suspensionDTO = new SuspensionDTO(suspension)
            {
                Tanks = suspension.TankSuspensions.Select(ts => new TankDTOTruncated(ts.Tank)).ToList()
            };

            return Ok(suspensionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post( string name, int tier, double loadLimit, int traverseSpeed,
                                                double weight, long price)
        {
            Suspension suspension = new Suspension();
            suspension.Name = name;
            suspension.Tier = tier;
            suspension.LoadLimit = loadLimit;
            suspension.TraverseSpeed = traverseSpeed;
            suspension.Weight = weight;
            suspension.Price = price;

            await _dbContext.Suspensions.AddAsync(suspension);
            await _dbContext.SaveChangesAsync();

            return Ok("Трансмисия додана.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string? name, int? tier, double? loadLimit,
                                            int? traverseSpeed, double? weight, long? price)
        {
            Suspension? susp = await _dbContext.Suspensions.FindAsync(id);

            if (susp == null) return NotFound("Suspension not found.");

            susp.Name = string.IsNullOrEmpty(name)?susp.Name : name;
            susp.Tier = (int)(tier ?? susp.Tier);
            susp.LoadLimit = (double)(loadLimit ?? susp.LoadLimit);
            susp.TraverseSpeed = (int)(traverseSpeed ?? susp.TraverseSpeed);
            susp.Weight = (double)(weight ?? susp.Weight);
            susp.Price = (long)(price ?? susp.Price);

            _dbContext.Suspensions.Update(susp);
            await _dbContext.SaveChangesAsync();

            return Ok("Update suspension.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dbContext.Suspensions.Where(s=>s.SuspensionId==id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return Ok("Delete suspension");
        }

    }
}
