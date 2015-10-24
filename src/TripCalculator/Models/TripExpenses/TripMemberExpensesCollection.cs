using System.Collections.Generic;

namespace TripCalculator.Models.TripExpenses
{
    public class TripMemberExpensesCollection
    {
        public IEnumerable<TripMemberExpenses> TripMemberExpenses { get; set; }
        public decimal TotalExpense { get; set; }
    }
}