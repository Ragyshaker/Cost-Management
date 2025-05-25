namespace ERPtask.models
{
    public class Notification
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public DateTime SentDate { get; set; }
        public string Message { get; set; }
    }
}
