using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MywebApi.Models
{
    public class Loginview
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [JsonIgnore]
        [Display(Name = "Remember me")]
       public bool RememberMe { get; set; }
    }
}
