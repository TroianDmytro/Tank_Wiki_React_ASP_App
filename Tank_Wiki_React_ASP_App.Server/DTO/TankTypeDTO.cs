using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class TankTypeDTO
    {
        public int TankTypeId { get; set; }
        public string TankType { get; set; }

        public TankTypeDTO(TankType tankType)
        {
            TankTypeId = tankType.TankTypeId;
            TankType = tankType.TypeMachine;
        }
    }
}
