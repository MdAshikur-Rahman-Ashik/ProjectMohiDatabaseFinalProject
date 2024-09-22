using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class ReplyAttachment
    {
        [Key]
        public int ReplyAttachID { get; set; }

        [ForeignKey(nameof(Reply))]
        public int ReplyID { get; set; }
        public virtual Reply Reply { get; set; }

        public string AttachFile { get; set; }

    }
}
