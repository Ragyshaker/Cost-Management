using ERPtask.servcies.Interfaces;

namespace ERPtask.HelperClasses
{
    public interface ITaxCalculator
    {
        (decimal TaxAmount, decimal Total) CalculateTax(decimal subtotal, decimal discounts, string region);
    }

    public class TaxCalculator : ITaxCalculator
    {
        private readonly ITaxRuleService _taxRuleService;

        public TaxCalculator(ITaxRuleService taxRuleService)
        {
            _taxRuleService = taxRuleService;
        }

        public (decimal TaxAmount, decimal Total) CalculateTax(decimal subtotal, decimal discounts, string region)
        {
            decimal taxRate;
            try
            {
                taxRate = _taxRuleService.GetTaxRateByRegion(region);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException($"No tax rule found for region: {region}");
            }

            var discountedSubtotal = subtotal - discounts;
            var taxAmount = discountedSubtotal * taxRate / 100;
            var total = discountedSubtotal + taxAmount;

            return (Math.Round(taxAmount, 2), Math.Round(total, 2));
        }
    }
}
