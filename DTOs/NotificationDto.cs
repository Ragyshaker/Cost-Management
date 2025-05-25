using System.ComponentModel.DataAnnotations;

namespace ERPtask.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public DateTime SentDate { get; set; }
        public string Message { get; set; }
    }
    public class NotificationCreateDto
    {
        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }
    }

    public class NotificationUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }
    }
}
