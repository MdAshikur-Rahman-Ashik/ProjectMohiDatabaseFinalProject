namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketSupportStatusHistoryDTOs
    {
        public int TicketSupportStatusHistoryID { get; set; }
        public int TicketSupportID { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; } // Assuming you want to include status name in the DTO
        public DateTime UpdatedAt { get; set; }
    }

    public class TicketSupportStatusHistoryCreateDTO
    {
        public int TicketSupportID { get; set; }
        public int StatusID { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


}
