using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    [PrimaryKey(nameof(SymbolCode), nameof(DateTime))]
    public class DayStatistics : IComparable<DayStatistics>
    {
        private Symbol _symbol;

        [Column(TypeName = "Date"), DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Column(TypeName = "decimal(13, 3)")]
        public decimal BuySellVolume { get; set; }

        public string Obj1 { get; set; } = string.Empty; // any object represented as xml/json

        public string Obj2 { get; set; } = string.Empty;

        public string Obj3 { get; set; } = string.Empty;

        public string Obj4 { get; set; } = string.Empty;

        public string Obj5 { get; set; } = string.Empty;

        // Foreigh key Symbol
        [JsonIgnore]
        public string SymbolCode { get; set; }
        [JsonIgnore]
        public Symbol Symbol { get => _symbol; set { SymbolCode = value.Code; _symbol = value; } }

        // Foreigh key Point
        public List<Point> Point { get; set; }

        public int CompareTo(DayStatistics? other)
        {
            if (other == null)
            {
                return 1;
            }
            return DateTime.CompareTo(other.DateTime);
        }
    }
}
