using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripPurchasesCollection
    {
        public IEnumerable<TripPurchases> Purchases { get; set; }
    }
}