namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketStatusDTOs
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
    public class TicketStatusCreateDTO
    {
        public string StatusName { get; set; }
    }

}
