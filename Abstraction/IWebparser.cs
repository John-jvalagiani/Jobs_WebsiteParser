using Jobs.Controllers;
using Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Abstraction
{
   public interface IWebparser
    {
      
       public Task<List<string>> GetPageData(string url);
     

    }
}
