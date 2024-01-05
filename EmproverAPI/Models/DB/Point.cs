using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
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
}
