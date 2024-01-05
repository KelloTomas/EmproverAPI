using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
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
}
