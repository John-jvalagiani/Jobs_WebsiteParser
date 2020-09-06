using Jobs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Exeptions
{
    public class StatementNotFoundExeption: Exception
    {
        public StatementNotFoundExeption()
        {
        }

        public StatementNotFoundExeption(string id)
            : base($"The statment with id {id} was not found")
        {
        }

        public StatementNotFoundExeption(string id, Exception inner)
            : base($"The statment with id {id} was not found", inner)
        {
        }

        
    }
}
