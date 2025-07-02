using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ticket_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTickets(int id)
        {
            var result = await _ticketsService.GetTickets(id);
            return Ok(result);
        }
    }
}
