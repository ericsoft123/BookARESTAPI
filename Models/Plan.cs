using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MywebApi.Models
{

    public class Plan
    {
        [JsonIgnore]
        [Key]
        public int id { get; set; }
        [Required]
        public string planId { get; set; }
        public string name { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public double price { get; set; }
    }
}
