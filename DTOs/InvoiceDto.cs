using System.ComponentModel.DataAnnotations;

namespace ERPtask.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Taxes { get; set; }
        public decimal Discounts { get; set; }
       // public List<InvoiceItemDto> Items { get; set; } 
    }
    public class InvoiceCreateDto
    {
        [Required]
        public int ClientId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Discounts { get; set; }

       // [MinLength(1)]
       // public List<InvoiceItemCreateDto> Items { get; set; }
    }
}
