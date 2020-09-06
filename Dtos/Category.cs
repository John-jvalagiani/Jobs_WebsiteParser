using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Dtos
{
    public class Category
    {

        public string Title { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string TermStart { get; set; }
        public string TermEnd { get; set; }
    }
}
