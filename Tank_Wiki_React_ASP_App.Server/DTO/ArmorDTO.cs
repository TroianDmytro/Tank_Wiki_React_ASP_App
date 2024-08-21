using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class ArmorDTO
    {
        public int ArmorId { get; set; }
        public string Name { get; set; }
        public int HullFront { get; set; } // Лобова броня корпусу
        public int HullSide { get; set; } // Бічна броня корпусу
        public int HullRear { get; set; } // Задня броня корпусу
        public TankDTOTruncated? Tanks { get; set; }

        public ArmorDTO()
        {

        }
        public ArmorDTO(Armor armor)
        {
            if (armor == null) return;
            ArmorId = armor.ArmorId;
            Name = armor.Name;
            HullFront = armor.HullFront;
            HullSide = armor.HullSide;
            HullRear = armor.HullRear;
        }
    }
}
