namespace ProjectMohiDatabase.Models
{
    public class ApplicationUser
    {
        public string ApplicationUserID { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<TicketSupport> TicketSupports { get; set; }
        public virtual ICollection<TicketManagement> TicketManagements { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        //
    }
}
