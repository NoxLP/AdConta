using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    public interface iBaja
    {
        bool Baja { get; }

        bool DarDeBaja();
        bool RecuperarBaja();
    }
}
