namespace TripCalculator.Models.TripExpenses
{
    public class TripSettlement
    {
        public TripGroupMember Payer { get; set; }
        public TripGroupMember Payee { get; set; }
        public decimal Amount { get; set; }
    }
}