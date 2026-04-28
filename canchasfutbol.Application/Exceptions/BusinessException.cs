using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Exceptions
{
    public class BusinessException : Exception 
    {
        public BusinessException() : base("Ocurrió un error de negocio") { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
