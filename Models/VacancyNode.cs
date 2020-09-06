using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Models
{
    public class VacancyNode
    {
        public HtmlNode Company { get; set; }
        public HtmlNode Titel { get; set; }
        public HtmlNode Date_Location { get; set; }
        public HtmlNode Custommer_Brand { get; set; }

    }
}
