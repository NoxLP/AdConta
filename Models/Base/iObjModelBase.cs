using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace AdConta.Models
{
    /// <summary>
    /// Interfaz de la que deben heredar todos los object models
    /// </summary>
    public interface iObjModelBase
    {
        int Id { get; }
    }
}
