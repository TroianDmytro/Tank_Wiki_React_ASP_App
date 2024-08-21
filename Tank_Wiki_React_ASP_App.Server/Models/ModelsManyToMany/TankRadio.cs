namespace Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany
{
    public class TankRadio
    {
        public int TankId { get; set; }
        public Tank Tank { get; set; }

        public int RadioId { get; set; }
        public Radio Radio { get; set; }

    }
}
