using AutoMapper;

using EFCore.BulkExtensions;

using EmproverAPI.Models;
using EmproverAPI.Models.DB;
using EmproverAPI.Models.Dto;
using EmproverAPI.Models.Enum;
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
        private readonly IMapper _mapper;

        public Market_ChartController(UserContext userContext, IMapper mapper)
        {
            _context = userContext;
            _mapper = mapper;
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
