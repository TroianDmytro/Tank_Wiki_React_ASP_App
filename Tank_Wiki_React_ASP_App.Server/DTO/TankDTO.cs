using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class TankDTO
    {
        public int TankId { get; set; } // Ідентифікатор
        public string Name { get; set; } // Назва
        //public int NationId { get; set; }//id нації
        //public Nations Nation { get; set; } // Нація
        public NationDTO Nation { get; set; }
        public int Tier { get; set; } // Рівень
        public int HitPoints { get; set; } // Очки міцності
        public bool Status { get; set; } //премiальний танк true
        /*public double Weight { get; set; }*/ //вага
        public long Price { get; set; }//Ціна
        public string? Description { get; set; }//опис
        public TankTypeDTO Type { get; set; } // Тип
        //public Armor Armor { get; set; } // Броня
        //public int ArmorId { get; set; } // Броня
        public ArmorDTO Armor { get; set; }
        public List<string> Crew { get; set; } // Екіпаж

        public List<TurretDTO> Turrets { get; set; }//Башні
        public List<EngineDTO> Engines { get; set; }//Двигуни
        public List<SuspensionDTO> Suspensions { get; set; }//Трансмісія
        public List<RadioDTO> Radios { get; set; }//Радіо

        public TankDTO()
        {
        }

        public TankDTO(Tank tank)
        {
            TankId = tank.TankId;
            Nation = new NationDTO(tank.Nation);
            Name = tank.Name;
            Tier = tank.Tier;
            HitPoints = tank.HitPoints;
            Status = tank.Status;
            Price = tank.Price;
            Description = tank.Description;
            Type = new TankTypeDTO(tank.TankType);
            Armor = new ArmorDTO(tank.Armor);
            Crew = tank.Crew;
        }
        public TankDTO(int tankId, string name, int nationId,
                        Nation nation, int tier, int hitPoints, bool status,
                        long price, string description, int typeId, TankType type,
                        Armor armor, int armorId, List<string> crew)
        {
            TankId = tankId;
            Name = name;
            //NationId = nationId;
            //Nation = nation;
            Nation = new NationDTO(nation);
            Tier = tier;
            HitPoints = hitPoints;
            Status = status;
            Price = price;
            Description = description;
            //TypeId = typeId;
            //Type = type;
            Type = new TankTypeDTO(type);
            //Armor = armor;
            //ArmorId = armorId;
            Crew = crew;
        }
    }
}
