namespace ProjectMohiDatabase.Models.DTOs
{
    public class ReplyDTOs
    {
        public int ReplyID { get; set; }
        public int TicketSupportID { get; set; }
        public int PersonID { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<int> ReplyAttachmentIds { get; set; }
    }
}
