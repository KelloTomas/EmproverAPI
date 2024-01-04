using EmproverAPI.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmproverAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Market_ChartController : Controller
    {
        private readonly UserContext _context;

        public Market_ChartController(UserContext userContext)
        {
            _context = userContext;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(_context.Users);
        }

        [HttpGet]
        public ActionResult GetAdmin()
        {
            return Ok(_context.Users.Where(u => u.Permissions == UserPermissions.Admin));
        }

        [HttpPost]
        public ActionResult AddIndicator([FromBody] Indicator indicator)
        {
            _context.Indicators.Add(indicator);
            return Ok();
        }

        [HttpPost]
        public ActionResult AddSymbol([FromBody] Symbol symbol)
        {
            _context.Symbols.Add(symbol);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("symbolCode")]
        public async Task<ActionResult> AddData(string symbolCode, [FromBody] List<DayStatisticsDto> data)
        {
            var symbol = await _context.Symbols
                                .Include(s => s.DayStatistics)
                                .ThenInclude(s => s.Point)
                                .FirstOrDefaultAsync(s => s.Code == symbolCode);

            if (symbol == null)
            {
                return NotFound($"Symbol with code {symbolCode} not found.");
            }

            foreach (var dayStatisticsDto in data)
            {
                var dayStatistics = new DayStatistics
                {
                    Kapitalizacia = dayStatisticsDto.Kapitalizacia,
                    BuySellVolume = dayStatisticsDto.BuySellVolume,
                    Point = new Point
                    {
                        DateTime = dayStatisticsDto.Point.DateTime,
                        OpenValue = dayStatisticsDto.Point.OpenValue,
                        CloseValue = dayStatisticsDto.Point.CloseValue,
                        MinValue = dayStatisticsDto.Point.MinValue,
                        MaxValue = dayStatisticsDto.Point.MaxValue
                    }
                };

                symbol.DayStatistics.Add(dayStatistics);
            }

            await _context.SaveChangesAsync();

            return Ok("DayStatistics added successfully");
        }
    }
}
