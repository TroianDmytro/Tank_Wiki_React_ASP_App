using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly db_TankWikiContext _dbContext;
        public PicturesController(db_TankWikiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPictures()
        {
            List<Picture> result = await _dbContext.Pictures.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicturesById(int id)
        {
            Picture picture = await _dbContext.Pictures.FirstOrDefaultAsync(x => x.PictureId == id);
            return Ok(picture);
        }

        [HttpPost]
        public async Task<IActionResult> PostPicture()
        {

        }

    }
}
