using System;
using System.ComponentModel;

namespace Jobs.Controllers
{
    public class Statement
    {   

        public Title Title { get; set; }
        public Company Company { get; set; }
        public string City { get; set; }
        public string TermStart { get; set; }
        public string TermEnd { get; set; }

    }

   public class Company
    {
        public string Name { get; set; }
        public string Logo { get; set; }
    }

   public class Title
    {
        public string JobTitel { get; set; }
        public string JobLink { get; set; }

    }

}