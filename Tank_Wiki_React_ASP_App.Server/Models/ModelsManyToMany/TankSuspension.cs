namespace Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany
{
    public class TankSuspension
    {
        public int TankId { get; set; }
        public Tank Tank { get; set; }

        public int SuspensionId { get; set; }
        public Suspension Suspension { get; set; }

    }
}
