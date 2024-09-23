namespace ProjectMohiDatabase.Models.DTOs
{
    public class ReplyDTOs
    {
        public int ReplyID { get; set; }
        public int TicketSupportID { get; set; }
        public string ApplicationUserID { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<int> ReplyAttachmentIds { get; set; }
    }
}
