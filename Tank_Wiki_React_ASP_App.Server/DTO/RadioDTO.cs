using Tank_Wiki_React_ASP_App.Server.Models;

namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class RadioDTO
    {
        public int RadioId { get; set; }
        public string Name { get; set; } // Назва

        public int Tier { get; set; } // Рівень
        public int SignalRange { get; set; } // Дальність сигналу
        public double Weight { get; set; } //вага
        public long Price { get; set; }//Ціна
        public List<TankDTOTruncated> Tanks { get; set; } = [];
        public RadioDTO()
        {

        }
        public RadioDTO(Radio radio)
        {
            RadioId = radio.RadioId;
            Name = radio.Name;
            Tier = radio.Tier;
            SignalRange = radio.SignalRange;
            Weight = radio.Weight;
            Price = radio.Price;
        }
        public RadioDTO(int radioId, string name, int tier,
                        int signalRange, double weight, long price)
        {
            RadioId = radioId;
            Name = name;
            Tier = tier;
            SignalRange = signalRange;
            Weight = weight;
            Price = price;
        }
    }
}
