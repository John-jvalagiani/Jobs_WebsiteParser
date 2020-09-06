using Jobs.Controllers;
using Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Abstraction
{
    public interface IJobswebparser
    {

        public Task<CustomerDetails> GetCustomerPageData(string url);

        public Task<StatementDetails> GetDetalsPageData(string url);

        public Task<Dictionary<string, List<Statement>>> GetMainPageData(string url, int index);

        public  Task<List<string>> GetDataByCategory(string url);
    }
}
