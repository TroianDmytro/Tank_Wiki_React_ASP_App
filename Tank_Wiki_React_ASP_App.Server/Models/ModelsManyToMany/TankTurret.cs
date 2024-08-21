namespace Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany
{
    public class TankTurret
    {
        public int TankId { get; set; }
        public Tank Tank { get; set; }

        public int TurretId { get; set; }
        public Turret Turret { get; set; }
    }
}
