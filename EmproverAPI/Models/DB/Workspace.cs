using System.ComponentModel.DataAnnotations;

namespace EmproverAPI.Models.DB
{
    public class Workspace
    {
        [Key]
        public int Id { get; set; }

        public Symbol Symbol { get; set; }

        public Indicator Indicator { get; set; }

        public List<FunctionParameterValue> FunctionParameterValues { get; set; }
    }
}
