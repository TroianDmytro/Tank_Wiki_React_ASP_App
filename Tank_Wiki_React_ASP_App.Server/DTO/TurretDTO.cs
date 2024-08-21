using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class TurretDTO
    {
        public int TurretId { get; set; }
        public string TurretName { get; set; }// Назва
        public int Tier { get; set; } // Рівень
        public int TurretFront { get; set; } // Лобова броня башти
        public int TurretSide { get; set; } // Бічна броня башти
        public int TurretRear { get; set; } // Задня броня башти
        public double Turn { get; set; }//поворот
        public int Overview { get; set; }//обзор
        public int Weight { get; set; } //вага
        public long Price { get; set; } //ціна
        public List<GunDTO>? Guns { get; set; } = [];
        public List<TankDTOTruncated>? Tanks { get; set; } = [];

        public TurretDTO()
        {

        }

        public TurretDTO(Turret turret)
        {
            TurretId = turret.TurretId;
            TurretName = turret.TurretName;
            Tier = turret.Tier;
            TurretFront = turret.TurretFront;
            TurretSide = turret.TurretSide;
            TurretRear = turret.TurretRear;
            Turn = turret.Turn;
            Overview = turret.Overview;
            Weight = turret.Weight;
            Price = turret.Price;
        }

        public TurretDTO(int turretId, string turretName,
                        int tier, int turretFront, int turretSide,
                        int turretRear, double turn, int overview,
                        int weight, long price)
        {
            TurretId = turretId;
            TurretName = turretName;
            Tier = tier;
            TurretFront = turretFront;
            TurretSide = turretSide;
            TurretRear = turretRear;
            Turn = turn;
            Overview = overview;
            Weight = weight;
            Price = price;
        }
    }
}
