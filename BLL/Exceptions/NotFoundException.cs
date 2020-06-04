using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
    class NotFoundException : Exception
    {
        public NotFoundException(string name)
            : base($"Entity \"{name}\" was not found.") { }

    }
}
