using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripMemberExpenses
    {
        public TripMember Member { get; set; }
        public IEnumerable<decimal> Expenses { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal AmountOwed { get; set; }
    }
}