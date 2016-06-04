#if (MCONTABILIDAD)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloContabilidad.Models;
using ModuloContabilidad.Models.Asientos;

namespace AdConta.ModelControl
{
    public partial class Comunidad
    {
        #region fields
        internal Dictionary<int, LedgeAccount> _CuentasContables = new Dictionary<int, LedgeAccount>();
        //internal Dictionary<int, aAsiento> _Asientos; <==== ¿Es necesario? Si lo es hay que añadirle tambien un ReadOnlyDictionary
        #endregion

        #region properties
        public ReadOnlyDictionary<int, LedgeAccount> CuentasContables { get; set; }
        #endregion

        #region helpers
        private void InitContabilidad()
        {
            this.CuentasContables = new ReadOnlyDictionary<int, LedgeAccount>(this._CuentasContables);
        }
        #endregion

        #region public methods
        #endregion
    }

}

#endif