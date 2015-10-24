using TripCalculator.Models.TripExpenses;

namespace TripCalculator.Services
{
    public interface ITripExpensesService
    {
        TripSettlementCollection GetSettlements(TripPurchasesCollection purchases);
    }

    public class TripExpensesService : ITripExpensesService
    {
        public TripSettlementCollection GetSettlements(TripPurchasesCollection purchases)
        {
            return new TripSettlementCollection();
        }
    }
}