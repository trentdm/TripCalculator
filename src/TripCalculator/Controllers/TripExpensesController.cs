using System;
using System.Net;
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
        public TripExpensesResponse Post([FromBody]TripMemberCollection query)
        {
            if (!ModelState.IsValid || query?.TripMembers == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                _logger.LogInfo("Received purchases: {0}", JsonConvert.SerializeObject(query));
                var settlements = _tripExpensesService.GetSettlements(query);
                _logger.LogInfo("Calculated settlements: {0}", JsonConvert.SerializeObject(settlements));
                
                return new TripExpensesResponse { Query = query, Data = settlements };
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
