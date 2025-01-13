using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LvrCalculatorWebApi.Data;
using LvrCalculatorWebApi.Models;
using System.Threading.Tasks;

namespace LvrCalculatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LvrCalculatorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LvrCalculatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/LvrCalculator
        [HttpPost]
        public async Task<IActionResult> CalculateLvr([FromBody] LvrRequest request)
        {
            // Validate the input parameters
            if (request.LoanAmount <= 0 || request.PropertyValue <= 0)
            {
                return BadRequest("Loan Amount and Property Value must be greater than 0.");
            }

            // Calculate LVR
            decimal lvr = (request.LoanAmount / request.PropertyValue) * 100;

            // Check if the LVR already exists in the database
            bool lvrExists = await _context.LvrEntries
                                            .AnyAsync(e => e.Lvr == lvr);

            if (lvrExists)
            {
                return Conflict("This LVR value already exists in the database.");
            }

            // Save the new LVR entry to the database
            var entry = new LvrEntry
            {
                LoanAmount = request.LoanAmount,
                PropertyValue = request.PropertyValue,
                Lvr = lvr
            };

            _context.LvrEntries.Add(entry);
            await _context.SaveChangesAsync();

            // Return the created entry with HTTP 201 Created
            return CreatedAtAction(nameof(CalculateLvr), new { id = entry.Id }, entry);
        }
    }

    public class LvrRequest
    {
        public decimal LoanAmount { get; set; }
        public decimal PropertyValue { get; set; }
    }
}
