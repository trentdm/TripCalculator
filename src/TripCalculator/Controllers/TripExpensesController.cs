using System.Web.Http;
using TripCalculator.Models.TripExpenses;
using TripCalculator.Services;

namespace TripCalculator.Controllers
{
    public class TripExpensesController : ApiController
    {
        private readonly ITripExpensesService _tripExpensesService;
        
        public TripExpensesController(ITripExpensesService tripExpensesService)
        {
            _tripExpensesService = tripExpensesService;
        }

        // POST: api/TripExpenses
        public TripExpensesResponse Post([FromBody]TripPurchasesCollection purchases)
        {
            var settlements = _tripExpensesService.GetSettlements(purchases);
            return new TripExpensesResponse { Purchases = purchases, Settlements = settlements };
        }
    }
}
