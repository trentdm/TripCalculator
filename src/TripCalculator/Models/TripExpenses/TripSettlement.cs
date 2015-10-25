namespace TripCalculator.Models.TripExpenses
{
    public class TripSettlement
    {
        public TripMember Sender { get; set; }
        public TripMember Receiver { get; set; }
        public decimal Amount { get; set; }
    }
}