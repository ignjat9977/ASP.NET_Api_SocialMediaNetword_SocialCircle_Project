using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(Type type)
        : base($"Entity of type {type.Name} its groupId was not found!")
        {

        }
    }
}
