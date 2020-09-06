using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Dtos
{
    public class StatementDetails
    {
        public string Description { get; set; }
        public string BrandLink { get; set; }
        public string BrandLogo { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
        {
        public string JobTitel { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPageLink { get; set; }
    }
}
