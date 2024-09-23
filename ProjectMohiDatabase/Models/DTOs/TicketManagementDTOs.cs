namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketManagementDTOs
    {
        public int TicketManagementID { get; set; }
        public int TicketSupportID { get; set; }
        public string AssignedTo { get; set; }
        public string ManagedByApplicationUserID { get; set; }
    }
    public class TicketManagementCreateDTO
    {
        public int TicketSupportID { get; set; }
        public string AssignedTo { get; set; }
        public string ManagedByApplicationUserID { get; set; }
    }

}
