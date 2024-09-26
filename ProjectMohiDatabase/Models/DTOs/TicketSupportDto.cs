namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketSupportDto
    {
        public int TicketSupportID { get; set; }
        public string ApplicationUserID { get; set; }
        public int PackageID { get; set; }
        public int StatusID { get; set; }
        public string Email { get; set; }
        public int PriorityID { get; set; }
        public int DepartmentID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public List<TicketAttachmentDto> TicketAttachments { get; set; }
        public List<ReplyDto> Replies { get; set; }
        public List<TicketManagementDto> TicketManagements { get; set; }
        public List<TicketSupportStatusHistoryDto> StatusHistories { get; set; }
    }

    public class TicketAttachmentDto
    {
        public int TicketAttachID { get; set; }
        public string AttachFile { get; set; }
        public int TicketSupportID { get; set; }
    }

    public class ReplyDto
    {
        public int TicketSupportID { get; set; }
        public int ReplyID { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ReplyAttachmentDto> ReplyAttachments { get; set; }
    }

    public class ReplyAttachmentDto
    {
        public int ReplyAttachID { get; set; }
        public string AttachFile { get; set; }
    }

    public class TicketManagementDto
    {
        public int TicketManagementID { get; set; }
        public string AssignedTo { get; set; }
        public int TicketSupportID { get; set; }
    }

    public class TicketSupportStatusHistoryDto
    {
        public DateTime UpdatedAt { get; set; }
        public int StatusID { get; set; }

    }
}
