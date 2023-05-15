using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string entityName, string propertyName, object value):
            base($"Entity {entityName} with {propertyName} {value.ToString()} is not found")
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }


    }
}
