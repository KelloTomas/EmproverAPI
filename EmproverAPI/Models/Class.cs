using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<DayStatistics> DayStatistics { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorParameter> IndicatorParameter { get; set; }
        public DbSet<AllowedValues> AllowedValues { get; set; }
    }
    [PrimaryKey(nameof(Id), nameof(DateTime))]
    public class Point
    {
        public int Id { get; set; }
        [Column(TypeName = "Date"), DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        public int OpenValue { get; set; }
        public int CloseValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
    public class PointDto : IValidDto
    {
        public DateTime DateTime { get; set; }
        public int OpenValue { get; set; }
        public int CloseValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public string IsValid()
        {
            return MinValue > MaxValue
                ? $"MinValue {MinValue} must be less then {MaxValue}"
                : string.Empty;
        }
    }

    public class Symbol
    {
        [Key]
        [StringLength(15)]
        public string Code { get; set; } = string.Empty;
        [StringLength(15)]
        public string Name { get; set; } = string.Empty; // can be EUR/USD, Gold, Silver, Cupprum
        [DataType(DataType.Date), Column(TypeName = "Date")]
        public DateTime ValidFrom { get; set; }
        [DataType(DataType.Date), Column(TypeName = "Date")]
        public DateTime ValidTo { get; set; }
        public List<DayStatistics> DayStatistics { get; set; }
    }

    public interface IValidDto
    {
        string IsValid();
    }

    public class SymbolDto : IValidDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // can be EUR/USD, Gold, Silver, Cupprum
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public List<DayStatisticsDto> DayStatistics { get; set; } = new List<DayStatisticsDto>();

        public string IsValid()
        {
            string errorMsg = string.Empty;
            int StringLength = 15; // ToDo make as constant
            if (Code.Length > StringLength)
            {
                errorMsg += $"Max Code lenght is {StringLength} but get {Code.Length}; ";
            }
            if (Name.Length > StringLength)
            {
                errorMsg += $"Max Name lenght is {StringLength} but get {Name.Length}; ";
            }
            if (ValidFrom > ValidTo)
            {
                errorMsg += $"ValidFrom {ValidFrom} is more then ValidTo {ValidTo}; ";
            }
            errorMsg += DayStatistics.Select(d => d.IsValid());

            return errorMsg;
        }
    }

    public class DayStatistics
    {
        [Key]
        public int Id { get; set; }
        public Point Point { get; set; } = new Point();
        public int Kapitalizacia { get; set; }
        public int BuySellVolume { get; set; }
        //public object? Obj1 { get; set; }
        //public object? Obj2 { get; set; }
        //public object? Obj3 { get; set; }
        //public object? Obj4 { get; set; }
        //public object? Obj5 { get; set; }
    }

    public class DayStatisticsDto : IValidDto
    {
        public PointDto Point { get; set; } = new PointDto();
        public int Kapitalizacia { get; set; }
        public int BuySellVolume { get; set; }

        public string IsValid()
        {
            return Point.IsValid();
        }
    }

    public class User
    {
        [Key]
        [StringLength(15)]
        public string Name { get; set; } = string.Empty;
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;
        [StringLength(15)]
        public string Password { get; set; } = string.Empty;
        public UserPermissions Permissions { get; set; }
    }

    public enum UserPermissions
    {
        Admin = 1,
        Trader = 2,
        User = 3
    }

    public class Indicator
    {
        [Key]
        [StringLength(15)]
        public string Code { get; set; } = string.Empty;
        [StringLength(15)]
        public string Name { get; set; } = string.Empty;
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;
        public List<IndicatorParameter> IndicatorParameters { get; set; } = new List<IndicatorParameter> { };
        public IndicatorDisplayType DisplayType { get; set; }
    }

    public class IndicatorParameter
    {
        [Key]
        [StringLength(15)]
        public string Name { get; set; } = string.Empty;
        public int Index { get; set; }
        public IndicatorDisplayType DisplayType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DefaultValue { get; set; }
        public List<AllowedValues> AllowedValues { get; set; } = new List<AllowedValues>();
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinValue { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaxValue { get; set; }
        public ParameterType ParameterType { get; set; }
        //public FunctionToDo Fce { get; set; } = new FunctionToDo();
    }

    public class AllowedValues
    {
        [Key]
        public int Key { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
    }

    //public class FunctionToDo
    //{
    //}

    public enum ParameterType
    {
        TypeInt = 1,
        TypeDecimal = 2
    }
    public enum IndicatorDisplayType
    {
        Line = 1,
        Spot = 2
    }
}
