using System.Web.Http;
using Newtonsoft.Json;
using TripCalculator.Models.TripExpenses;
using TripCalculator.Services;

namespace TripCalculator.Controllers
{
    public class TripExpensesController : ApiController
    {
        private readonly ITripExpensesService _tripExpensesService;
        private readonly ILogger _logger;

        public TripExpensesController(ITripExpensesService tripExpensesService, ILogger logger)
        {
            _tripExpensesService = tripExpensesService;
            _logger = logger;
        }

        // POST: api/TripExpenses
        public TripExpensesResponse Post([FromBody]TripMemberExpensesCollection memberExpenses)
        {
            _logger.LogInfo("Received purchases: {0}", JsonConvert.SerializeObject(memberExpenses));

            var settlements = _tripExpensesService.GetSettlements(memberExpenses);

            _logger.LogInfo("Calculated settlements: {0}", JsonConvert.SerializeObject(settlements));

            return new TripExpensesResponse { TripMemberExpenses = memberExpenses, Settlements = settlements };
        }
    }
}
