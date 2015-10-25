using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TripCalculator.Models.TripExpenses
{
    public class TripMember
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<decimal> Expenses { get; set; }

        [JsonIgnore]
        public decimal TotalExpense { get; set; }

        [JsonIgnore]
        public decimal AmountOwed { get; set; }

        [JsonIgnore]
        public decimal AmountTransferred { get; set; }

        [JsonIgnore]
        public decimal AmountBalance { get; set; }
    }
}