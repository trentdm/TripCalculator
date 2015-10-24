using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripSettlementCollection
    {
        public IEnumerable<TripSettlement> Settlements { get; set; }
    }
}