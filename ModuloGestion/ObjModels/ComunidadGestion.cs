using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace ModuloGestion.ObjModels
{
    public class ComunidadGestion
    {
        public ComunidadGestion(Date primeraFecha, int ownerIdCdad)
        {
            this.PrimeraFecha = new ReadOnlyDate(primeraFecha);
            this.OwnerIdCdad = ownerIdCdad;
        }

        #region fields
        #endregion

        #region properties
        public int OwnerIdCdad { get; private set; }
        public ReadOnlyDate PrimeraFecha { get; private set; }
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
