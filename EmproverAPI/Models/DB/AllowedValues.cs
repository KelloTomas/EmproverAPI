using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    public class AllowedValues
    {
        [Key]
        public int Key { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
    }
}
