using Jobs.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Dtos
{
    public class CustomerDetails
    {
        public Dictionary<string,List<Statement>> CustomerStatements { get; set; }

        public string CustomerName { get; set; }
        public string CustomerLogo { get; set; }
    }
}
