namespace LvrCalculatorWebApi.Models
{
    public class LvrEntry
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal PropertyValue { get; set; }
        public decimal Lvr { get; set; }
    }
}
