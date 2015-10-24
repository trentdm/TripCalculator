namespace TripCalculator.Models.TripExpenses
{
    public class TripExpensesResponse
    {
        public TripMemberExpensesCollection TripMemberExpenses { get; set; }
        public TripSettlementCollection Settlements { get; set; }
    }
}