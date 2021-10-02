using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.api.Model
{
    public class ValidaCampoViweModelOutput
    {
        public IEnumerable<string> Erros { get; set; }

        public ValidaCampoViweModelOutput(IEnumerable<string>erros)
        {
            Erros = erros;
        }
    }
}
