using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MywebApi.Models
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Erros { get; set; }
        public DateTime? Expired { get; set; }
    }
}
