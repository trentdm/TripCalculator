namespace TripCalculator.Models.TripExpenses
{
    public class TripSettlement
    {
        public TripMember Payer { get; set; }
        public TripMember Payee { get; set; }
        public decimal Amount { get; set; }
    }
}