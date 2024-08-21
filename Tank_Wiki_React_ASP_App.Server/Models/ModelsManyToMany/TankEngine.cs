namespace Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany
{
    public class TankEngine
    {
        public int TankId { get; set; }
        public Tank Tank { get; set; }

        public int EngineId { get; set; }
        public Engine Engine { get; set; }

    }
}
