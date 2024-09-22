namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketSupportStatusHistoryDTOs
    {
       
            public int TicketSupportID { get; set; }
            public int StatusID { get; set; }
            public string StatusName { get; set; }
            public DateTime UpdatedAt { get; set; }
        

    }

    public class TicketSupportStatusHistoryCreateDTO
    {
        public int TicketSupportID { get; set; }
        public int StatusID { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
