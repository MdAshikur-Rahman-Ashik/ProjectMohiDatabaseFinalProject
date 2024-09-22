namespace ProjectMohiDatabase.Models.DTOs
{
    public class ReplyAttachmentDTOs
    {
        public int ReplyAttachID { get; set; }
        public int ReplyID { get; set; }
        public string AttachFileUrl { get; set; }
    }
    public class ReplyAttachmentCreateDTOs
    {
        public int ReplyID { get; set; }
        public IFormFile AttachFile { get; set; }
    }
}
