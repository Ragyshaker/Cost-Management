using System.ComponentModel.DataAnnotations;

namespace ERPtask.DTOs
{
    public class TaxRuleDto
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public decimal TaxRate { get; set; }
    }
    public class TaxRuleCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Region { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal TaxRate { get; set; }
    }

    public class TaxRuleUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Region { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal TaxRate { get; set; }
    }
}
