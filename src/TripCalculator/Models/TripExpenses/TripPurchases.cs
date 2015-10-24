using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripPurchases
    {
        public TripGroupMember GroupMember { get; set; }
        public IEnumerable<decimal> Purchases { get; set; }
    }
}