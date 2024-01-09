using System.ComponentModel.DataAnnotations;
using EmproverAPI.Models.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    public class Indicator
    {
        [Key]
        [StringLength(15)]
        public string Code { get; set; } = string.Empty;

        [StringLength(15)]
        public string Name { get; set; } = string.Empty;

        [StringLength(150)]
        public string Description { get; set; } = string.Empty;

        public IndicatorDisplayTypeEnum DisplayType { get; set; }
        
        public Function Function { get; set; }
    }
}
