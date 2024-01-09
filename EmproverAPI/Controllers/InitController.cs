using AutoMapper;

using EFCore.BulkExtensions;

using EmproverAPI.Models;
using EmproverAPI.Models.DB;
using EmproverAPI.Models.Enum;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmproverAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InitController : Controller
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public InitController(UserContext userContext, IMapper mapper)
        {
            _context = userContext;
            _mapper = mapper;
        }

        [HttpPut]
        public ActionResult InitUsers()
        {
            if (_context.Users.Any())
            {
                return BadRequest("Already exist");
            }

            var users = new List<User>
            {
                new User()
                {
                    Name = "T",
                    Description = "TT",
                    Password = "Password",
                    Permissions = UserPermissionsEnum.Admin
                },
                new User()
                {
                    Name = "A",
                    Description = "AA",
                    Password = "AAA",
                    Permissions = UserPermissionsEnum.Trader
                },
                new User()
                {
                    Name = "B",
                    Description = "BB",
                    Password = "BBB",
                    Permissions = UserPermissionsEnum.User
                }
            };
            _context.BulkInsert(users);
            return Ok();
        }

        [HttpPut]
        public ActionResult InitSymbols()
        {
            if (_context.Symbols.Any())
            {
                return BadRequest("Already exist");
            }

            var symbols = new List<Symbol>
            {
                new Symbol()
                {
                    Code = "EUR",
                    Name = "EUR",
                    ValidFrom = new DateTime(2022, 1, 1),
                    ValidTo = new DateTime(2022, 1, 5),
                    DayStatistics = new List<DayStatistics>
                    {
                        new DayStatistics()
                        {
                            BuySellVolume = 5,
                            DateTime = new DateTime(2022,1,1),
                            Point = new List<Point>
                            {
                                new Point()
                                {
                                    OpenValue = 3,
                                    CloseValue = 1,
                                    MaxValue = 5,
                                    MinValue = 0,
                                    Time = new DateTime(2022,1,1)
                                }
                            }
                        },
                        new DayStatistics()
                        {
                            BuySellVolume = 5,
                            DateTime = new DateTime(2022,1,2),
                            Point = new List<Point>
                            {
                                new Point()
                                {
                                    OpenValue = 3,
                                    CloseValue = 1,
                                    MaxValue = 5,
                                    MinValue = 0,
                                    Time = new DateTime(2022,1,2)
                                }
                             }
                        },
                        new DayStatistics()
                        {
                            BuySellVolume = 5,
                            DateTime = new DateTime(2022,1,3),
                            Point = new List<Point>
                            {
                                new Point()
                                {
                                    OpenValue = 3,
                                    CloseValue = 1,
                                    MaxValue = 5,
                                    MinValue = 0,
                                    Time = new DateTime(2022,1,3)
                                }
                            }
                        }
                    }
                },
                new Symbol()
                {
                    Code = "USD",
                    Name = "USD",
                    ValidFrom = new DateTime(2023, 1, 1),
                    ValidTo = new DateTime(2023, 1, 5),
                    DayStatistics = new List<DayStatistics>
                    {
                        new DayStatistics()
                        {
                            BuySellVolume = 5,
                            DateTime = new DateTime(2022,1,1),
                            Point = new List<Point>
                            {
                                new Point()
                                {
                                    OpenValue = 3,
                                    CloseValue = 1,
                                    MaxValue = 5,
                                    MinValue = 0,
                                    Time = new DateTime(2022,1,1)
                                }
                            }
                        },
                        new DayStatistics()
                        {
                            BuySellVolume = 5,
                            DateTime = new DateTime(2022,1,4),
                            Point = new List<Point>
                            {
                                new Point()
                                {
                                    OpenValue = 3,
                                    CloseValue = 1,
                                    MaxValue = 5,
                                    MinValue = 0,
                                    Time = new DateTime(2022,1,4)
                                }
                            }
                        },
                        new DayStatistics()
                        {
                            BuySellVolume = 5,
                            DateTime = new DateTime(2022,1,5),
                            Point = new List<Point>
                            {
                                new Point()
                                {
                                    OpenValue = 3,
                                    CloseValue = 1,
                                    MaxValue = 5,
                                    MinValue = 0,
                                    Time = new DateTime(2022,1,5),
                                }
                            }
                        }
                    }
                }
            };
            symbols.ForEach(s => s.DayStatistics
                .ForEach(d =>
                {
                    d.Symbol = s;
                    d.Point.ForEach(p => p.DayStatistics = d);
                }
            ));

            _context.Symbols.AddRange(symbols);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult InitIndicator()
        {
            if (_context.Indicators.Any())
            {
                return BadRequest("Already exist");
            }

            var functions = new List<Function>()
            {
                new Function()
                {
                    Name = "Fce1",
                    Parameters = new List<FunctionParameter>()
                        {
                            new FunctionParameter()
                            {
                                Index = 0,
                                Name = "Name0",
                                MinValue=0,
                                MaxValue=5,
                                DefaultValue = 0,
                                ParameterType = ParameterTypeEnum.TypeInt
                            },
                            new FunctionParameter()
                            {
                                Index = 1,
                                Name = "Name1",
                                MinValue=10,
                                MaxValue=15,
                                DefaultValue = 10,
                                ParameterType = ParameterTypeEnum.TypeInt
                            }
                        }
                },
                new Function()
                {
                    Name = "Fce10",
                    Parameters = new List<FunctionParameter>()
                        {
                            new FunctionParameter()
                            {
                                Index = 1,
                                Name = "Name10",
                                MinValue=0,
                                MaxValue=5,
                                DefaultValue = 0,
                                ParameterType = ParameterTypeEnum.TypeDecimal
                            },
                            new FunctionParameter()
                            {
                                Index = 2,
                                Name = "Name11",
                                MinValue=10,
                                MaxValue=15,
                                DefaultValue = 10,
                                ParameterType = ParameterTypeEnum.TypeDecimal
                            }
                        }
                }
            };
            functions.ForEach(f => f.Parameters.ForEach(p => p.Function = f));

            var indicators = new List<Indicator>()
            {
                new Indicator()
                {
                    Code = "Ind_1",
                    Name = "Ind_1",
                    Description = "Ind_1",
                    Function = functions[0],
                    DisplayType = IndicatorDisplayTypeEnum.Line
                },
                new Indicator()
                {
                    Code = "Ind_2",
                    Name = "Ind_2",
                    Description = "Ind_2",
                    Function = functions[0],
                    DisplayType = IndicatorDisplayTypeEnum.Line
                },
                new Indicator()
                {
                    Code = "Ind_3",
                    Name = "Ind_3",
                    Description = "Ind_3",
                    Function = functions[1],
                    DisplayType = IndicatorDisplayTypeEnum.Line
                }
            };

            _context.Indicators.AddRange(indicators);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult InitWorkspace()
        {
            if (_context.Workspaces.Any())
            {
                return BadRequest("Already exist");
            }

            var symbol = _context.Symbols.First(s => s.Name == "USD");
            var indicator = _context.Indicators
                            .Include(i => i.Function)
                            .ThenInclude(f => f.Parameters)
                            .First(s => s.Name == "Ind_1");
            var functionParameters = indicator.Function.Parameters;

            var workspaces = new List<Workspace>
            { 
                new Workspace
                {
                    //Id = 1,
                    Symbol = symbol,
                    Indicator = indicator,
                    FunctionParameterValues = new List<FunctionParameterValue>
                    {
                        new FunctionParameterValue
                        {
                            Value = 77,
                            ParameterDefinition = functionParameters.First(p => p.Index == 1),

                        },
                        new FunctionParameterValue
                        {
                            Value = 88,
                            ParameterDefinition = functionParameters.First(p => p.Index == 2),
                        }
                    }
                }
            };

            workspaces.ForEach(w => w.FunctionParameterValues.ForEach(v => v.Workspace = w));

            _context.Workspaces.AddRange(workspaces);
            _context.SaveChanges();

            return Ok();
        }
    }
}
