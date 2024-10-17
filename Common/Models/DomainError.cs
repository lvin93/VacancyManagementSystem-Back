using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DomainError
    {
        public string? ErrorMessage { get; set; }
        public int? StatusCode { get; set; }
        public string? InnerException { get; set; }
    }
}
