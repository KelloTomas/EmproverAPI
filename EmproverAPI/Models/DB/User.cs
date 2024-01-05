using System.ComponentModel.DataAnnotations;
using EmproverAPI.Models.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    public class User
    {
        [Key]
        [StringLength(15)]
        public string Name { get; set; } = string.Empty;
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;
        [StringLength(15)]
        public string Password { get; set; } = string.Empty;
        public UserPermissionsEnum Permissions { get; set; }
    }
}
