using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using EmproverAPI.Models.Enum;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    [PrimaryKey(nameof(FunctionName), nameof(Index))]
    public class FunctionParameter
    {
        private Function function;

        [StringLength(15)]
        public string Name { get; set; } = string.Empty;

        public int Index { get; set; }

        [Column(TypeName = "decimal(10, 3)")]
        public decimal? DefaultValue { get; set; }

        public List<AllowedValues> AllowedValues { get; set; } = new List<AllowedValues>();

        [Column(TypeName = "decimal(10, 3)")]
        public decimal? MinValue { get; set; }

        [Column(TypeName = "decimal(10, 3)")]
        public decimal? MaxValue { get; set; }

        public ParameterTypeEnum ParameterType { get; set; }

        [JsonIgnore]
        public string FunctionName { get; set; }

        [JsonIgnore]
        public Function Function { get => function; set { FunctionName = value.Name; function = value; } }
    }
}
