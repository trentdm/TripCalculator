namespace TripCalculator.Models.TripExpenses
{
    public class TripExpensesResponse
    {
        public TripPurchasesCollection Purchases { get; set; }
        public TripSettlementCollection Settlements { get; set; }
    }
}