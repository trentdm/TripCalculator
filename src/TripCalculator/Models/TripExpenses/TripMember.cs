using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripMember
    {
        public string Name { get; set; }
        public IEnumerable<decimal> Expenses { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal AmountTransferred { get; set; }
        public decimal AmountBalance { get; set; }
    }
}