using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class TankDTOTruncated
    {
        public int TankId { get; set; }
        public string TankName { get; set; }
        public TankDTOTruncated(Tank tank)
        {
            TankId = tank.TankId;
            TankName = tank.Name;
        }
    }
}
