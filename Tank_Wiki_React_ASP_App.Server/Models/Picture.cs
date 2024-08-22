using System.ComponentModel.DataAnnotations;

namespace Tank_Wiki_React_ASP_App.Server.Models
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
