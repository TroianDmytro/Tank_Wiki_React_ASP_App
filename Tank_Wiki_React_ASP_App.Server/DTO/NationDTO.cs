namespace Tank_Wiki_React_ASP_App.Server.DTO
{
    public class NationDTO
    {
        public int NationId { get; set; }
        public string NationName { get; set; }
        public ICollection<TankDTOTruncated> Tanks { get; set; }

        public NationDTO(Nations nations)
        {
            NationId = nations.NationId;
            NationName = nations.NationName;
        }
        public NationDTO(int nationId, string nationName)
        {
            NationId = nationId;
            NationName = nationName;
        }
    }
}
