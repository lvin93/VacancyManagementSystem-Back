using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public DomainError Error { get; set; }
    }
}
