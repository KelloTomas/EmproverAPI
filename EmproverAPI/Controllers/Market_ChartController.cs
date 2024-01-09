using AutoMapper;

using EFCore.BulkExtensions;

using EmproverAPI.Models;
using EmproverAPI.Models.DB;
using EmproverAPI.Models.Dto;
using EmproverAPI.Models.Enum;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmproverAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Market_ChartController : Controller
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public Market_ChartController(UserContext userContext, IMapper mapper)
        {
            _context = userContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetWorkspaces(string userName)
            => Ok();

        [HttpPost]
        public ActionResult AddUpdateWorkspace(string userName)
            => Ok();

        [HttpPost]
        public ActionResult DeleteWorkspace(string userName)
            => Ok();

        [HttpGet]
        public ActionResult GetSymbols(string userName)
            => Ok();

        [HttpPost]
        public ActionResult AddUpdateSymbol(string userName)
            => Ok();

        [HttpGet]
        public ActionResult Query(int workspaceId, DateTime startDate, DateTime? endDate)
        {
            DateTime entDateNotNull = endDate ?? DateTime.Now;
            var workspace = _context.Workspaces
                                .Include(w => w.Symbol)
                                .ThenInclude(s => s.DayStatistics)
                                .ThenInclude(d => d.Point)
                                .FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null)
            {
                return NotFound(workspaceId);
            }

            // ToDo mozno dake obmedzenie na priliz vela datumov ak chce ziskat. Obrazovka aj tak dokaze zobrazil len obmedzene mnozstvo

            var data = workspace.Symbol.DayStatistics
                        .Where(d => d.DateTime >= startDate
                                    && d.DateTime <= endDate).ToList();

            FillMissingDates(data, startDate, entDateNotNull);

            data.Sort();

            return Ok(data);
        }

        private void FillMissingDates(List<DayStatistics> data, DateTime startDate, DateTime endDate)
        {
            // Create a list of all dates within the range
            List<DateTime> allDatesInRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            List<DateTime> missingDates = allDatesInRange.Except(data.Select(d => d.DateTime)).ToList();

            data.AddRange(missingDates.Select(d =>
                    new DayStatistics()
                    {
                        DateTime = d,
                        BuySellVolume = 0,
                        Point = new List<Point> { new Point() { Time = d } }
                    })
                );
        }



















        // some testing endpoints
        [HttpGet]
        public ActionResult GetAvailableSymbols(string traderName)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == traderName);
            if (user == null)
            {
                return NotFound(traderName);
            }
            if (user.Permissions == UserPermissionsEnum.User)
            {
                return Unauthorized();
            }
            return Ok(_context.Symbols.Include(s => s.DayStatistics));
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(_context.Users);
        }

        [HttpGet]
        public ActionResult GetAdmin()
        {
            return Ok(_context.Users.Where(u => u.Permissions == UserPermissionsEnum.Admin));
        }

        [HttpPost]
        public ActionResult AddIndicator([FromBody] Indicator indicator)
        {
            _context.Indicators.Add(indicator);
            return Ok();
        }

        [HttpPost]
        public ActionResult AddSymbol([FromBody] SymbolDto symbolDto)
        {
            if (symbolDto == null)
            {
                return NotFound($"Symbol not provided.");
            }

            string errMsg = symbolDto.IsValid();
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                return ValidationProblem(errMsg);
            }

            var symbol = _mapper.Map<Symbol>(symbolDto);

            _context.Symbols.Add(symbol);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public ActionResult AddSymbols([FromBody] List<SymbolDto> symbolsDto)
        {
            if (symbolsDto == null || symbolsDto.Count == 0)
            {
                return NotFound($"Symbol not provided.");
            }

            string errMsg = string.Join(';', symbolsDto.Select(s => s.IsValid()));
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                return ValidationProblem(errMsg);
            }

            var symbols = _mapper.Map<IEnumerable<Symbol>>(symbolsDto);

            _context.BulkInsert(symbols);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("symbolCode")]
        public async Task<ActionResult> AddData(string symbolCode, [FromBody] List<DayStatisticsDto> dayStatisticsDto)
        {
            var symbol = await _context.Symbols
                                .Include(s => s.DayStatistics)
                                .ThenInclude(s => s.Point)
                                .FirstOrDefaultAsync(s => s.Code == symbolCode);

            if (symbol == null)
            {
                return NotFound($"Symbol with code {symbolCode} not found.");
            }

            var dayStatistics = _mapper.Map<IEnumerable<DayStatistics>>(dayStatisticsDto);
            symbol.DayStatistics.AddRange(dayStatistics);
            await _context.SaveChangesAsync();
            return Ok("DayStatistics added successfully");
        }
    }
}
