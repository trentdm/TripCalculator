using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TripCalculator.Models.TripExpenses
{
    public class TripMemberCollection
    {
        [Required]
        public IEnumerable<TripMember> TripMembers { get; set; }

        [JsonIgnore]
        public decimal TotalExpense { get; set; }
    }
}