namespace Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany
{
    public class TankPicture
    {
        public int TankId { get; set; }
        public Tank Tank { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}

