using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class EngineDTO
    {
        public int EngineId { get; set; }
        public string Name { get; set; } // Назва
        public int Tier { get; set; } // Рівень
        public int Power { get; set; } // Потужність
        public double FireChance { get; set; } // Ймовірність загоряння
        public double Weight { get; set; } //вага
        public long Price { get; set; }//Ціна
        public ICollection<TankDTOTruncated>? Tanks { get; set; } = [];
        public EngineDTO()
        {

        }

        public EngineDTO(Engine engine)
        {
            EngineId = engine.EngineId;
            Name = engine.Name;
            Tier = engine.Tier;
            Power = engine.Power;
            FireChance = engine.FireChance;
            Weight = engine.Weight;
            Price = engine.Price;
        }
        public EngineDTO(int engineId, string name,
                        int tier, int power, double fireChance,
                        double weight, long price, ICollection<TankDTO>? tanks)
        {
            EngineId = engineId;
            Name = name;
            Tier = tier;
            Power = power;
            FireChance = fireChance;
            Weight = weight;
            Price = price;
        }
    }
}
