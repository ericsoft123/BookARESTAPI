using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MywebApi.Models
{
    public class Subscription
    {
        [JsonIgnore]
        public int id { get; set; }
        [JsonIgnore]
        public string email { get; set; }//Email/username of who purchased subscription

        public string planId { get; set; }
        [JsonIgnore]
        public string name { get; set; }//name of book
        [JsonIgnore]
        public double price { get; set; }
   
        public string text { get; set; }
       
        [DataType(DataType.Date)]

        public DateTime Created_at { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime End_at { get; set; }
    }
}
