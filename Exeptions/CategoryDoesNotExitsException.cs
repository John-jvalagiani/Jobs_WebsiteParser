using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Exeptions
{
    public class CategoryDoesNotExitsException: Exception
    {
        public CategoryDoesNotExitsException()
        {
        }

        public CategoryDoesNotExitsException(string name)
            : base($"The category with name {name} doesnot exits")
        {
        }

        public CategoryDoesNotExitsException(string name, Exception inner)
            : base($"The category with name {name} doesnot exits", inner)
        {
        }

    }
}
