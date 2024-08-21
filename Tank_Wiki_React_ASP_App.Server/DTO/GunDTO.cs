using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class GunDTO
    {
        public int GunId { get; set; }
        public int Tier { get; set; } // Рівень
        public string Name { get; set; } // Назва
        public int Penetration { get; set; } // Пробивна здатність
        public int Damage { get; set; } // Шкода
        public double RateOfFire { get; set; } // Швидкість стрільби
        public double Accuracy { get; set; } // Точність
        public double AimTime { get; set; } // Час прицілювання
        public int Ammunition { get; set; }//боекомплект
        public double Weight { get; set; } //вага
        public long Price { get; set; }//Ціна

        public List<TurretDTO>? Turret { get; set; } = [];

        public GunDTO() { }
        public GunDTO(Gun gun)
        {
            GunId = gun.GunId;
            Tier = gun.Tier;
            Name = gun.Name;
            Penetration = gun.Penetration;
            Damage = gun.Damage;
            RateOfFire = gun.RateOfFire;
            Accuracy = gun.Accuracy;
            AimTime = gun.AimTime;
            Ammunition = gun.Ammunition;
            Weight = gun.Weight;
            Price = gun.Price;
        }
    }
}
