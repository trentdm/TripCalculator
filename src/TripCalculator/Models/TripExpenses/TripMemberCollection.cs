using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripMemberCollection
    {
        public IEnumerable<TripMember> TripMembers { get; set; }
        public decimal TotalExpense { get; set; }
    }
}