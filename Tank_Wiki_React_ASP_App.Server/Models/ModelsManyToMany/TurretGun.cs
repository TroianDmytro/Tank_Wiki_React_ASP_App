namespace Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany
{
    public class TurretGun
    {
        public int TurretId { get; set; }
        public Turret Turret { get; set; }

        public int GunId { get; set; }
        public Gun Gun { get; set; }

    }
}
