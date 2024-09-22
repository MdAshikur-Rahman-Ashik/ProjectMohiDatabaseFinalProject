namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketSupportDTOs
    {
      
            public int TicketSupportID { get; set; }
            public int PersonID { get; set; }
            public int PackageID { get; set; }
            public int StatusID { get; set; }
            public string Email { get; set; }
            public int PriorityID { get; set; }
            public int DepartmentID { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
            public ICollection<TicketAttachmentDTOs> TicketAttachments { get; set; }
            public ICollection<TicketSupportStatusHistoryDTOs> TicketSupportStatusHistories { get; set; }
            public ICollection<TicketManagementDTOs> TicketManagements { get; set; }
            public ICollection<ReplyDTOs> Replies { get; set; }
        

    }

    public class TicketSupportCreateDTO
    {
        public int PersonID { get; set; }
        public int PackageID { get; set; }
        public int StatusID { get; set; }
        public string Email { get; set; }
        public int PriorityID { get; set; }
        public int DepartmentID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }

}
