using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmproverAPI.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    [PrimaryKey(nameof(Name), nameof(Index))]
    public class IndicatorParameter
    {
        [StringLength(15)]
        public string Name { get; set; } = string.Empty;
        public int Index { get; set; }
        public IndicatorDisplayTypeEnum DisplayType { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? DefaultValue { get; set; }
        public List<AllowedValues> AllowedValues { get; set; } = new List<AllowedValues>();
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? MinValue { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? MaxValue { get; set; }
        public ParameterTypeEnum ParameterType { get; set; }
        //public FunctionToDo Fce { get; set; } = new FunctionToDo();
    }
}
