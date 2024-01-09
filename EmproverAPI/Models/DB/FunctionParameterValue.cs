using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace EmproverAPI.Models.DB
{
    [PrimaryKey(nameof(WorkspaceId), nameof(FunctionParameterIndex), nameof(FunctionParameterFunctionName))]
    public class FunctionParameterValue
    {
        private Workspace workspace;
        private FunctionParameter parameterDefinition;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }

        [JsonIgnore]
        public int WorkspaceId { get; set; }

        [JsonIgnore]
        public Workspace Workspace 
        { 
            get => workspace;
            set 
            {
                WorkspaceId = value.Id; 
                workspace = value; 
            }
        }

        public int FunctionParameterIndex { get; set; }

        public string FunctionParameterFunctionName { get; set; }

        public FunctionParameter ParameterDefinition
        {
            get => parameterDefinition;
            set
            {
                FunctionParameterIndex = value.Index;
                FunctionParameterFunctionName = value.FunctionName;
                parameterDefinition = value;
            }
        }

        public bool IsValid()
            => (ParameterDefinition.MinValue ?? Value) <= Value
               && (ParameterDefinition.MinValue ?? Value) >= Value
               && (ParameterDefinition.AllowedValues.Any(v => v.Value == Value)
                   || ParameterDefinition.AllowedValues.IsNullOrEmpty());
    }
}
