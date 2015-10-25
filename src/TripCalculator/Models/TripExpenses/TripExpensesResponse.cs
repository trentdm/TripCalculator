namespace TripCalculator.Models.TripExpenses
{
    public class TripExpensesResponse
    {
        public TripMemberCollection Query { get; set; }
        public TripSettlementCollection Data { get; set; }
    }
}