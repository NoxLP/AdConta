using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdConta.Models;

namespace AdConta
{
    public interface iAppRepositories
    {
        Repository<Persona> PersonasRepository { get; }
        Repository<Concepto> ConceptosRepository { get; }

    }
}
