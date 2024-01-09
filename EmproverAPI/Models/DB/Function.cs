using System.ComponentModel.DataAnnotations;

namespace EmproverAPI.Models.DB
{
    public class Function
    {
        [Key]
        public string Name { get; set; } = string.Empty;

        public List<FunctionParameter> Parameters { get; set; } = new List<FunctionParameter>();
    }
}