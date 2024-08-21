using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class SuspensionDTO
    {
        public int SuspensionId { get; set; }
        public int Tier { get; set; } // Рівень
        public string Name { get; set; } // Назва
        public double LoadLimit { get; set; } // Обмеження навантаження
        public int TraverseSpeed { get; set; } // Швидкість повороту
        public double Weight { get; set; } //вага
        public long Price { get; set; }//Ціна
        public List<TankDTOTruncated> Tanks { get; set; } = [];

        public SuspensionDTO()
        {

        }
        public SuspensionDTO(Suspension suspension)
        {
            SuspensionId = suspension.SuspensionId;
            Tier = suspension.Tier;
            Name = suspension.Name;
            LoadLimit = suspension.LoadLimit;
            TraverseSpeed = suspension.TraverseSpeed;
            Weight = suspension.Weight;
            Price = suspension.Price;
        }

        public SuspensionDTO(int suspensionId,
                            int tier,
                            string name,
                            int loadLimit,
                            int traverseSpeed,
                            double weight,
                            long price)
        {
            SuspensionId = suspensionId;
            Tier = tier;
            Name = name;
            LoadLimit = loadLimit;
            TraverseSpeed = traverseSpeed;
            Weight = weight;
            Price = price;
        }
    }
}
