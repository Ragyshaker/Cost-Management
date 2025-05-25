using ERPtask.DTOs;

namespace ERPtask.models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Taxes { get; set; }
        public decimal Discounts { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
