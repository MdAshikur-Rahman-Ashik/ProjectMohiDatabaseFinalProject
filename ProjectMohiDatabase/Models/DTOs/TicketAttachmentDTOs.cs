using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models.DTOs
{
    public class TicketAttachmentDTOs
    {
     
        public int TicketAttachID { get; set; }
        public int TicketSupportID { get; set; }
 
        public string AttachFile { get; set; }
    }
    public class TicketAttachmentCreateDTOs
    {
        public int TicketSupportID { get; set; }
        public IFormFile AttachFile { get; set; }
    }
}
